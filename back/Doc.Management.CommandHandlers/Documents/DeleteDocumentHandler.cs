using System.Threading;
using System.Threading.Tasks;
using Doc.Management.CQRS;
using Doc.Management.Documents;
using Doc.Management.Documents.Commands;
using Doc.Management.ValueObjects;

namespace Doc.Management.CommandHandlers.Documents
{
    internal class DeleteDocumentHandler : SingleAggregateCommandHandler<DeleteDocument, Document>
    {
        public DeleteDocumentHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader)
            : base(eventWriter, aggregateReader) { }

        protected override AggregateResult ExecuteCommand(
            Document aggregate,
            DeleteDocument command,
            UserId ownerId
        ) => aggregate.Delete(ownerId);

        protected override Task<Document?> LoadAggregate(
            DeleteDocument command,
            UserId ownerId,
            CancellationToken cancellationToken
        ) => AggregateReader.LoadAsync<Document>(command.Id, cancellationToken: cancellationToken);
    }
}
