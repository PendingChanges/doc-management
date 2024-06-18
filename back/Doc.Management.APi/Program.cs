using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Doc.Management.CommandHandlers;
using Serilog.Formatting.Compact;
using Serilog;
using Doc.Management.Marten;
using Doc.Management;
using Journalist.Crm.Api.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Doc.Management.S3;
using Microsoft.Extensions.Configuration;

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

builder.Services.AddCommandHandlers()
                .AddDocManagementMarten(configuration)
                .AddS3(s3Options);

builder.Services.AddHttpContextAccessor()
                     .AddTransient<IContext, Context>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
