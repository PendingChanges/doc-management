using Doc.Management.Documents;
using Marten;

namespace Journalist.Crm.Marten.Clients;

public class DocumentRepository : IReadDocuments
{
    private readonly IQuerySession _session;

    public DocumentRepository(IQuerySession session)
    {
        _session = session;
    }
}
