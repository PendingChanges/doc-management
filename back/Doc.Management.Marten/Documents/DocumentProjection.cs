using System.Threading.Tasks;
using Doc.Management.Documents.DataModels;
using Doc.Management.Documents.Events;
using Marten;
using Marten.Events;
using Marten.Events.Projections;

namespace Doc.Management.Marten.Documents;

public class DocumentProjection : EventProjection
{
    public static DocumentDocument Create(DocumentCreated documentCreated) =>
        new(
            documentCreated.Id,
            documentCreated.Key,
            documentCreated.Name,
            documentCreated.FileNameWithoutExtension,
            documentCreated.Extension,
            documentCreated.Version
        );

    public static void Project(DocumentDeleted @event, IDocumentOperations ops) =>
        ops.Delete<DocumentDocument>(@event.Id);

    public static async Task Project(DocumentModified @event, IDocumentOperations ops)
    {
        var document = await ops.Query<DocumentDocument>()
            .SingleOrDefaultAsync(c => c.Id == @event.Id);

        if (document != null)
        {
            var documentUpdated = document with
            {
                Name = @event.Name,
                Extension = @event.Extension,
                Key = @event.Key,
                FileNameWithoutExtension = @event.FileNameWithoutExtension,
                Version = @event.Version
            };

            ops.Store(documentUpdated);
        }
    }
}
