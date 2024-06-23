using Doc.Management.Documents.DataModels;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doc.Management.Documents;

public interface IReadDocuments
{
    Task<DocumentDocument?> GetDocumentByIdAsync(string id, Version? version, CancellationToken cancellationToken = default);
}
