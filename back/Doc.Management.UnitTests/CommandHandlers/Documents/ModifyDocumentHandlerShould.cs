using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Doc.Management.CommandHandlers;
using Doc.Management.CommandHandlers.Documents;
using Doc.Management.CQRS;
using Doc.Management.Documents;
using Doc.Management.Documents.Commands;
using Doc.Management.ValueObjects;
using Moq;
using Xunit;

namespace Doc.Management.UnitTests.CommandHandlers.Documents;

public class ModifyDocumentHandlerShould
{
    private readonly Mock<IWriteEvents> _eventWriterMock;
    private readonly Mock<IReadAggregates> _aggregateReaderMock;

    public ModifyDocumentHandlerShould()
    {
        _eventWriterMock = new Mock<IWriteEvents>();
        _aggregateReaderMock = new Mock<IReadAggregates>();
    }

    [Fact]
    public async Task Handle_modify_command_and_update_document_properly()
    {
        // Arrange
        var ownerId = new UserId("user id");
        var aggregate = new Document();
        aggregate.CreateDocument(
            DocumentKey.NewDocumentKey(),
            "name",
            "filename",
            "ext",
            VersionIncrementType.Major,
            ownerId
        );
        _aggregateReaderMock
            .Setup(_ =>
                _.LoadAsync<Document>(
                    It.IsAny<Guid>(),
                    It.IsAny<int?>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(aggregate);
        var handler = new ModifyDocumentHandler(
            _eventWriterMock.Object,
            _aggregateReaderMock.Object
        );
        var command = new ModifyDocument(
            aggregate.Id,
            DocumentKey.NewDocumentKey(),
            "new name",
            "newfilename",
            "newext",
            VersionIncrementType.Major
        );
        var wrappedCommand = new WrappedCommand<ModifyDocument, Document>(command, ownerId);

        // Act
        var aggregateInReturn = await handler.Handle(wrappedCommand, CancellationToken.None);

        // Assert
        _eventWriterMock.Verify(_ =>
            _.StoreAsync(
                aggregateInReturn.Id,
                aggregateInReturn.Version,
                It.IsAny<IEnumerable<object>>(),
                It.IsAny<CancellationToken>()
            )
        );
    }
}
