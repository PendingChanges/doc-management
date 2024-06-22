using System.Threading;
using System.Threading.Tasks;
using Doc.Management.Documents.Commands;
using Doc.Management.Documents;
using Doc.Management.CQRS;
using Doc.Management.ValueObjects;

namespace Doc.Management.CommandHandlers.Documents
{
    internal class CreateDocumentHandler : SingleAggregateCommandHandler<CreateDocument, Document>
    {
        public CreateDocumentHandler(IWriteEvents eventWriter, IReadAggregates aggregateReader) : base(eventWriter, aggregateReader) { }

        protected override AggregateResult ExecuteCommand(Document aggregate, CreateDocument command, UserId ownerId) => aggregate.Create(command.Key, command.Name, command.FileNameWithoutExtension, command.Extension, ownerId);

        protected override Task<Document?> LoadAggregate(CreateDocument command, UserId ownerId,
            CancellationToken cancellationToken) => Task.FromResult<Document?>(new Document());
    }
}
