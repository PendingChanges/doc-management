using System;
using System.Threading;
using System.Threading.Tasks;
using Doc.Management.Documents;
using Doc.Management.Documents.DataModels;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Doc.Management.GraphQL.Documents;

[ExtendObjectType("Query")]
public class DocumentsQueries
{
    [GraphQLName("allDocuments")]
    [UseOffsetPaging(IncludeTotalCount = true)]
    public async Task<CollectionSegment<Outputs.Document>> GetAllDocumentsAsync(
        [Service] IReadDocuments documentsReader,
        int? skip,
        int? take,
        string? sortBy,
        string? sortDirection,
        CancellationToken cancellationToken = default
    )
    {
        var request = new GetDocumentsRequest(skip, take, sortBy, sortDirection);
        var documentResultSet = await documentsReader.GetDocumentsAsync(request, cancellationToken);

        var pageInfo = new CollectionSegmentInfo(
            documentResultSet.HasNextPage,
            documentResultSet.HasPreviousPage
        );

        var collectionSegment = new CollectionSegment<Outputs.Document>(
            documentResultSet.Data.ToDocuments(),
            pageInfo,
            ct => ValueTask.FromResult((int)documentResultSet.TotalItemCount)
        );

        return collectionSegment;
    }

    [GraphQLName("document")]
    public async Task<Outputs.Document?> GetDocument(
        [Service] IReadDocuments documentReader,
        Guid id,
        string? version,
        CancellationToken cancellationToken = default
    )
    {
        var document = await documentReader.GetDocumentByIdAsync(
            id,
            version != null ? Version.Parse(version) : null,
            cancellationToken
        );

        return document.ToDocumentOrNull();
    }
}
