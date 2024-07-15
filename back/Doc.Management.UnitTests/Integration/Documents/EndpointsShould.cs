using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Threading.Tasks;
using Alba;
using Xunit;

namespace Doc.Management.UnitTests.Integration.Documents;

[Collection("integration")]
public class EndpointsShould
{
    private const string DocumentApiUrl = "/api/documents";

    [Fact]
    public async Task Return_Documents()
    {
        await using var host = await AlbaHost.For<global::Program>();

        // This runs an HTTP request and makes an assertion
        // about the expected content of the response
        await host.Scenario(_ =>
        {
            _.Get.Url(DocumentApiUrl);
            _.StatusCodeShouldBeOk();
        });
    }

    [Fact]
    public async Task CreateDocument_Then_GetIt()
    {
        await using var host = await AlbaHost.For<global::Program>();
        await using var pdfFile = File.OpenRead("test.pdf");
        var pdfFileName = Path.GetFileName(pdfFile.Name);

        using var content = new StreamContent(pdfFile);
        content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Pdf);
        using var formData = new MultipartFormDataContent();
        formData.Add(content, "uploadFile", pdfFileName);

        // This runs an HTTP request and makes an assertion
        // about the expected content of the response
        var result = await host.Scenario(_ =>
        {
            _.Post.MultipartFormData(formData)
                .QueryString("versionIncrementType", "Major")
                .ToUrl(DocumentApiUrl);
            _.StatusCodeShouldBe(201);
        });
    }
}
