using System;
using System.Text.Json.Serialization;
using Doc.Management;
using Doc.Management.Api;
using Doc.Management.Api.Documents;
using Doc.Management.Api.Infrastructure;
using Doc.Management.CommandHandlers;
using Doc.Management.Documents;
using Doc.Management.GraphQL;
using Doc.Management.Marten;
using Doc.Management.S3;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Formatting.Compact;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(new RenderedCompactJsonFormatter())
    .CreateLogger();

try
{
    Log.Information("Starting web application");
    var MyAllowSpecificOrigins = "LocalOnly";

    var builder = WebApplication.CreateBuilder(args);
    var configuration = builder.Configuration;
    var authenticationOptions = configuration
        .GetSection(KeycloakAuthenticationOptions.Section)
        .Get<KeycloakAuthenticationOptions>();

    builder.Host.UseSerilog();

    var s3Options = new S3Options();
    var s3ConfigurationSection = builder.Configuration.GetSection(S3Options.S3);
    s3ConfigurationSection.Bind(s3Options);

    builder.Services.Configure<S3Options>(s3ConfigurationSection);

    builder
        .Services.AddCommandHandlers()
        .AddDocManagementMarten(configuration)
        .AddS3(s3Options)
        .AddDocManagementGraphQL()
        .AddHealthChecks();

    builder.Services.AddHttpContextAccessor().AddTransient<IContext, Context>();

    builder
        .Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddAntiforgery();

    builder.Services.AddKeycloackAuthentication(authenticationOptions!);

    builder.Services.AddAuthorization(
        (options) =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            options.AddPolicy("Administrators", policy => policy.RequireClaim("roles", "[admin]"));
            options.AddPolicy("Users", policy => policy.RequireClaim("roles", "[user]"));
        }
    );

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(
            MyAllowSpecificOrigins,
            policy =>
            {
                policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            }
        );
    });

    var app = builder.Build();
    app.UseAntiforgery();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseCors(MyAllowSpecificOrigins);
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseDefaultFiles();
    app.UseStaticFiles();
    app.MapGraphQL();
    app.MapDocuments();

    app.MapWhen(
        context =>
            !(context.Request.Path.Value ?? string.Empty).Contains("/graphql")
            && !(context.Request.Path.Value ?? string.Empty).Contains("/healthz")
            && !(context.Request.Path.Value ?? string.Empty).Contains("/api"),
        app =>
        {
            app.Use(
                    (context, next) =>
                    {
                        context.Request.Path = "/index.html";
                        return next();
                    }
                )
                .UseStaticFiles();
        }
    );
    app.MapHealthChecks("/healthz");

    using (var serciceScope = app.Services.CreateScope())
    {
        var fileStore = serciceScope.ServiceProvider.GetRequiredService<IStoreFile>();
        IOptionsSnapshot<S3Options> s3OptionsSnapshot =
            serciceScope.ServiceProvider.GetRequiredService<IOptionsSnapshot<S3Options>>();

        await fileStore.CreateBucketAsync(s3OptionsSnapshot.Value.BucketName ?? "document-storage");
    }

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    await Log.CloseAndFlushAsync();
}
