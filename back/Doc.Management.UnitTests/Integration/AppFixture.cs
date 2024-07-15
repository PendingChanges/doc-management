using System;
using System.IO;
using System.Threading.Tasks;
using Alba;
using Marten;
using Microsoft.AspNetCore.Hosting;
using Xunit;

namespace Doc.Management.UnitTests.Integration;

public class AppFixture : IAsyncLifetime
{
    private string SchemaName { get; } =
        "sch" + Guid.NewGuid().ToString().Replace("-", string.Empty);
    public IAlbaHost? Host { get; private set; }

    public async Task InitializeAsync()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
        Environment.SetEnvironmentVariable(
            "ConnectionStrings__Marten",
            $"Host=localhost;Port=5433;Username=postgres;Password=postgres"
        );
        Environment.SetEnvironmentVariable(
            "S3__ServiceUrl",
            $"http://s3.us-east-2.localhost.localstack.cloud:4566"
        );
        Environment.SetEnvironmentVariable("S3__AccessKey", $"test");
        Environment.SetEnvironmentVariable("S3__SecretKey", $"test");
        Environment.SetEnvironmentVariable("S3__BucketName", $"document-storage");

        Host = await AlbaHost.For<Program>(b =>
        {
            b.ConfigureServices(
                (context, services) =>
                {
                    services.ConfigureMarten(s =>
                    {
                        s.DatabaseSchemaName = SchemaName;
                    });
                }
            );
        });
    }

    public async Task DisposeAsync()
    {
        await Host!.DisposeAsync();
    }
}
