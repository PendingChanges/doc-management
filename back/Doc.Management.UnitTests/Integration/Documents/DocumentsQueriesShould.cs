using System.Threading.Tasks;
using Alba;
using Xunit;

namespace Doc.Management.UnitTests.Integration.Documents;

[Collection("integration")]
public class DocumentsQueriesShould
{
    private const string DocumentGraphQLUrl = "/graphql/";

    [Fact]
    public async Task Return_Documents()
    {
        await using var host = await AlbaHost.For<global::Program>();

        var query =
            @"query {
  allDocuments(skip: 0, take: 50) {
    items {
      extension
      fileNameWithoutExtension
      id
      key
      name
      version {
        build
        major
        majorRevision
        minor
        minorRevision
        revision
      }
    }
  }
}";

        // This runs an HTTP request and makes an assertion
        // about the expected content of the response
        await host.Scenario(_ =>
        {
            _.Post.Json(query);
            _.Post.Url(DocumentGraphQLUrl);
            _.StatusCodeShouldBeOk();
        });
    }
}
