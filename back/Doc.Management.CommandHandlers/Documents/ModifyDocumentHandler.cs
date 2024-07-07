using System.Threading;
using System.Threading.Tasks;
using Doc.Management.CQRS;
using Doc.Management.Documents;
using Doc.Management.Documents.Commands;
using Doc.Management.ValueObjects;

namespace Doc.Management.CommandHandlers.Documents
{
    internal class ModifyDocumentHandler : SingleAggregateCommandHandler<ModifyDocument, Document>
    {
        public ModifyDocumentHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader)
            : base(eventWriter, aggregateReader) { }

        protected override AggregateResult ExecuteCommand(
            Document aggregate,
            ModifyDocument command,
            UserId ownerId
        ) =>
            aggregate.Modify(
                command.Key,
                command.Name,
                command.FileNameWithoutExtension,
                command.Extension,
                command.VersionIncrementType,
                ownerId
            );

        protected override Task<Document?> LoadAggregate(
            ModifyDocument command,
            UserId ownerId,
            CancellationToken cancellationToken
        ) =>
            AggregateReader.LoadAsync<Document>(
                command.DocumentId,
                cancellationToken: cancellationToken
            );
    }
}
