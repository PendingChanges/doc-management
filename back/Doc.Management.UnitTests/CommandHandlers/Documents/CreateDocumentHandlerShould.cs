using Moq;
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

public class CreateDocumentHandlerShould
{
    private readonly Mock<IWriteEvents> _eventWriterMock;
    private readonly Mock<IReadAggregates> _aggregateReaderMock;

    public CreateDocumentHandlerShould()
    {
        _eventWriterMock = new Mock<IWriteEvents>();
        _aggregateReaderMock = new Mock<IReadAggregates>();
    }

    [Fact]
    public async Task Handle_wrapped_command_create_document_properly()
    {
        //Arrange
        var ownerId = new UserId("user id");
        var handler = new CreateDocumentHandler(_eventWriterMock.Object, _aggregateReaderMock.Object);
        var command = new CreateDocument(DocumentKey.Parse("key"), "name", "ext");
        var wrappedCommand = new WrappedCommand<CreateDocument, Document>(command, ownerId);

        //Act
        await handler.Handle(wrappedCommand, CancellationToken.None);

        //Assert
        Assert.True(true);
    }
}
