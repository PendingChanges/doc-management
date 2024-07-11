using System.Threading.Tasks;
using Alba;
using Xunit;

namespace Doc.Management.UnitTests.Integration.Documents;

[Collection("integration")]
public class EndpointsShould
{
    [Fact]
    public async Task Return_Documents()
    {
        await using var host = await AlbaHost.For<global::Program>();

        // This runs an HTTP request and makes an assertion
        // about the expected content of the response
        await host.Scenario(_ =>
        {
            _.Get.Url("/api/documents");
            _.StatusCodeShouldBeOk();
        });
    }
}
