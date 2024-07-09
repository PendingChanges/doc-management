using AutoFixture;
using Doc.Management.Documents.DataModels;
using Doc.Management.Documents.Events;
using Doc.Management.Marten.Documents;
using Marten;
using Moq;
using Xunit;

namespace Doc.Management.UnitTests.Marten.Documents;
public class DocumentProjectionShould
{
    [Fact]
    public void Create()
    {
        //Arrange
        var fixture = new Fixture();
        var documentCreated = fixture.Create<DocumentCreated>();

        //Act
        var document = DocumentProjection.Create(documentCreated);

        //Assert
        Assert.NotNull(document);
        Assert.Equal(documentCreated.Id, document.Id);
        Assert.Equal(documentCreated.Name, document.Name);
        Assert.Equal(documentCreated.Key, document.Key);
        Assert.Equal(documentCreated.Extension, document.Extension);
        Assert.Equal(documentCreated.FileNameWithoutExtension, document.FileNameWithoutExtension);
        Assert.Equal(documentCreated.Version, document.Version);
    }

    [Fact]
    public void Delete()
    {
        //Arrange
        var fixture = new Fixture();
        var documentDeleted = fixture.Create<DocumentDeleted>();
        var documentOperationsMock = new Mock<IDocumentOperations>();
        documentOperationsMock.Setup(_ => _.Delete<DocumentDocument>(documentDeleted.Id)).Verifiable();

        //Act
        DocumentProjection.Project(documentDeleted, documentOperationsMock.Object);

        //Assert
        documentOperationsMock.VerifyAll();
    }
}
