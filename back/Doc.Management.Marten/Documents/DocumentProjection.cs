using Doc.Management.Documents.DataModels;
using Doc.Management.Documents.Events;
using Marten;
using Marten.Events.Projections;

namespace Doc.Management.Marten.Documents;

public class DocumentProjection : EventProjection
{
    public static DocumentDocument Create(DocumentCreated documentCreated)
        => new(documentCreated.Id, documentCreated.Key, documentCreated.Name, documentCreated.FileNameWithoutExtension, documentCreated.Extension, documentCreated.Version);

    public static void Project(DocumentDeleted @event, IDocumentOperations ops)
        => ops.Delete<DocumentDocument>(@event.Key);
}
