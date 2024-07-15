using System;
using System.Threading;
using System.Threading.Tasks;
using Doc.Management.Documents.DataModels;

namespace Doc.Management.Documents;

public interface IReadDocuments
{
    Task<DocumentDocument?> GetDocumentByIdAsync(
        Guid id,
        Version? version,
        CancellationToken cancellationToken = default
    );

    Task<DocumentResultSet> GetDocumentsAsync(
        GetDocumentsRequest request,
        CancellationToken cancellationToken = default
    );
}
