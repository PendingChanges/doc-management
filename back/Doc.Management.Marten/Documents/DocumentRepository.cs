using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Doc.Management.Documents;
using Doc.Management.Documents.DataModels;
using Marten;
using Marten.Linq;
using Marten.Pagination;

namespace Doc.Management.Marten.Documents;

public class DocumentRepository : IReadDocuments
{
    private readonly IQuerySession _session;

    public DocumentRepository(IQuerySession session)
    {
        _session = session;
    }

    public Task<DocumentDocument?> GetDocumentByIdAsync(
        string id,
        Version? version,
        CancellationToken cancellationToken = default
    )
    {
        var query = _session.Query<DocumentDocument>().Where(d => d.Id == id);

        if (version != null)
        {
            query = query.Where(d => d.Version == version);
        }

        return query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<DocumentResultSet> GetDocumentsAsync(
        GetDocumentsRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var query = _session.Query<DocumentDocument>().Where(d => true);

        query = SortBy(request, query);

        var pagedResult = await query.ToPagedListAsync(
            request.Skip + 1,
            request.Take,
            cancellationToken
        );

        return new DocumentResultSet(
            pagedResult.ToList(),
            pagedResult.TotalItemCount,
            pagedResult.HasNextPage,
            pagedResult.HasPreviousPage
        );
    }

    private static IQueryable<DocumentDocument> SortBy(
        GetDocumentsRequest request,
        IQueryable<DocumentDocument> query
    ) =>
        request.SortDirection switch
        {
            "desc"
                => request.SortBy switch
                {
                    _ => query.OrderByDescending(c => c.Name)
                },
            _
                => request.SortBy switch
                {
                    _ => query.OrderBy(c => c.Name)
                },
        };
}
