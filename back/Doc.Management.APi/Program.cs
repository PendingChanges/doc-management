using System.Text.Json.Serialization;
using Doc.Management;
using Doc.Management.Api.Documents;
using Doc.Management.Api.Infrastructure;
using Doc.Management.CommandHandlers;
using Doc.Management.Documents;
using Doc.Management.Marten;
using Doc.Management.S3;
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

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Host.UseSerilog();

var s3Options = new S3Options();
var s3ConfigurationSection = builder.Configuration.GetSection(S3Options.S3);
s3ConfigurationSection.Bind(s3Options);

builder.Services.Configure<S3Options>(s3ConfigurationSection);

builder.Services.AddCommandHandlers().AddDocManagementMarten(configuration).AddS3(s3Options);

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

var app = builder.Build();
app.UseAntiforgery();
app.MapDocuments();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

using (var serciceScope = app.Services.CreateScope())
{
    var fileStore = serciceScope.ServiceProvider.GetRequiredService<IStoreFile>();
    IOptionsSnapshot<S3Options> s3OptionsSnapshot = serciceScope.ServiceProvider.GetRequiredService<
        IOptionsSnapshot<S3Options>
    >();

    await fileStore.CreateBucketAsync(s3OptionsSnapshot.Value.BucketName ?? "document-storage");
}

await app.RunAsync();
