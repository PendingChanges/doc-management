using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Doc.Management.CommandHandlers;
using Serilog.Formatting.Compact;
using Serilog;
using Doc.Management.Marten;
using Doc.Management;
using Journalist.Crm.Api.Infrastructure;

Log.Logger = new LoggerConfiguration()
          .WriteTo.Console(new RenderedCompactJsonFormatter())
          .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Host.UseSerilog();

builder.Services.AddCommandHandlers()
                .AddDocManagementMarten(configuration);

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
