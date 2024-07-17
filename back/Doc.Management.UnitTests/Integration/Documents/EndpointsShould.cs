using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Threading.Tasks;
using Alba;
using Doc.Management.Documents.DataModels;
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
    public async Task Document_Lifecycle()
    {
        await using var host = await AlbaHost.For<global::Program>();
        await using var pdfFile = File.OpenRead("test.pdf");
        var pdfFileName = Path.GetFileName(pdfFile.Name);

        using var content = new StreamContent(pdfFile);
        content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Pdf);
        using var formData = new MultipartFormDataContent();
        formData.Add(content, "uploadFile", pdfFileName);

        var createResult = await host.Scenario(_ =>
        {
            _.Post.MultipartFormData(formData)
                .QueryString("versionIncrementType", "Major")
                .ToUrl(DocumentApiUrl);
            _.StatusCodeShouldBe(201);
        });

        var documentCreated = await createResult.ReadAsJsonAsync<DocumentDocument>();

        Assert.NotNull(documentCreated);

        var getResult = await host.Scenario(_ =>
        {
            _.Get.Url($"{DocumentApiUrl}/{documentCreated.Id}/infos");
            _.StatusCodeShouldBeOk();
        });

        var documentRetrieved = await getResult.ReadAsJsonAsync<DocumentDocument>();

        Assert.NotNull(documentRetrieved);
        Assert.Equal(documentCreated, documentRetrieved);

        await host.Scenario(_ =>
        {
            _.Put.MultipartFormData(formData)
                .QueryString("versionIncrementType", "Major")
                .ToUrl($"{DocumentApiUrl}/{documentCreated.Id}");
            _.StatusCodeShouldBeOk();
        });

        var getResult2 = await host.Scenario(_ =>
        {
            _.Get.Url($"{DocumentApiUrl}/{documentCreated.Id}/infos");
            _.StatusCodeShouldBeOk();
        });

        var documentRetrieved2 = await getResult2.ReadAsJsonAsync<DocumentDocument>();

        Assert.NotNull(documentRetrieved2);
        Assert.Equal(new Version(2, 0), documentRetrieved2.Version);

        await host.Scenario(_ =>
        {
            _.Delete.Url($"{DocumentApiUrl}/{documentCreated.Id}");
            _.StatusCodeShouldBeOk();
        });

        var getResult3 = await host.Scenario(_ =>
        {
            _.Get.Url($"{DocumentApiUrl}/{documentCreated.Id}/infos");
            _.StatusCodeShouldBe(404);
        });
    }
}
