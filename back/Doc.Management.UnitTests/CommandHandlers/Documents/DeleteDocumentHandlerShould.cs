using System.Collections.Generic;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Doc.Management.Documents.Commands;
using Doc.Management.Documents;
using Doc.Management.CQRS;
using Doc.Management.ValueObjects;
using Doc.Management.CommandHandlers;
using Doc.Management.CommandHandlers.Documents;

namespace Doc.Management.UnitTests.CommandHandlers.Documents;

public class DeleteDocumentHandlerShould
{
    private readonly Mock<IWriteEvents> _eventWriterMock;
    private readonly Mock<IReadAggregates> _aggregateReaderMock;

    public DeleteDocumentHandlerShould()
    {
        _eventWriterMock = new Mock<IWriteEvents>();
        _aggregateReaderMock = new Mock<IReadAggregates>();
    }

    [Fact]
    public async Task Handle_wrapped_command_delete_document_properly()
    {
        //Arrange
        var ownerId = new UserId("user id");
        var aggregate = new Document();
        aggregate.Create(DocumentKey.NewDocumentKey(), "name", "filename", "ext", ownerId);
        _aggregateReaderMock.Setup(_ => _.LoadAsync<Document>(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<CancellationToken>())).ReturnsAsync(aggregate);
        var handler = new DeleteDocumentHandler(_eventWriterMock.Object, _aggregateReaderMock.Object);
        var command = new DeleteDocument(aggregate.Id);
        var wrappedCommand = new WrappedCommand<DeleteDocument, Document>(command, ownerId);

        //Act
        var aggregateInReturn = await handler.Handle(wrappedCommand, CancellationToken.None);

        //Assert
        _eventWriterMock.Verify(_ => _.StoreAsync(aggregateInReturn.Id, aggregateInReturn.Version, It.IsAny<IEnumerable<object>>(), It.IsAny<CancellationToken>()));
    }

    [Fact]
    public async Task Throw_domain_exception_when_aggregate_does_not_exists()
    {
        //Arrange
        var ownerId = new UserId("user id");
        var aggregateId = EntityId.NewEntityId();
        _aggregateReaderMock.Setup(_ => _.LoadAsync<Document>(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<CancellationToken>())).ReturnsAsync((Document?)null);
        var handler = new DeleteDocumentHandler(_eventWriterMock.Object, _aggregateReaderMock.Object);
        var command = new DeleteDocument(aggregateId);
        var wrappedCommand = new WrappedCommand<DeleteDocument, Document>(command, ownerId);

        //Act
        var exception = await Assert.ThrowsAsync<DomainException>(() => handler.Handle(wrappedCommand, CancellationToken.None));

        //Assert
        Assert.Single(exception.DomainErrors);
        var domainError = exception.DomainErrors.FirstOrDefault();
        Assert.NotNull(domainError);
        if (domainError != null)
        {
            Assert.Equal(Errors.AggregateNotFound.CODE, domainError.Code);
        }
    }
}
