using Doc.Management.Documents;
using Doc.Management.Documents.DataModels;
using Marten;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Journalist.Crm.Marten.Clients;

public class DocumentRepository : IReadDocuments
{
    private readonly IQuerySession _session;

    public DocumentRepository(IQuerySession session)
    {
        _session = session;
    }

    public Task<DocumentDocument?> GetDocumentByIdAsync(string id, Version? version, CancellationToken cancellationToken = default)
    {
        var query = _session.Query<DocumentDocument>().Where(d => d.Id == id);

        if(version != null)
        {
            query = query.Where(d => d.Version  == version);
        }

        return query.FirstOrDefaultAsync(cancellationToken);
    }
}
