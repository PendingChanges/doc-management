using System;
using System.Linq;
using Doc.Management.Documents;
using Doc.Management.Documents.Events;
using Doc.Management.ValueObjects;
using TechTalk.SpecFlow;
using Xunit;

namespace Doc.Management.UnitTests.Domain.Documents;

[Binding]
public class DocumentStepDefinitions
{
    private readonly AggregateContext _aggregateContext;

    public DocumentStepDefinitions(AggregateContext aggregateContext)
    {
        _aggregateContext = aggregateContext;
    }

    [Given(@"No existing document")]
    public static void GivenNoExistingDocument()
    {
        //Nothing to do here
    }

    [When(
        @"A user with id ""([^""]*)"" create a document with key ""([^""]*)"" name ""([^""]*)"", filename ""([^""]*)"" and extension ""([^""]*)"""
    )]
    public void WhenAUserWithIdCreateADocumentWithKeyNameFilenameAndExtension(
        string userId,
        string key,
        string name,
        string file,
        string ext
    )
    {
        var aggregate = new Document();
        _aggregateContext.Result = aggregate.CreateDocument(
            DocumentKey.Parse(key),
            name,
            file,
            ext,
            Management.Documents.Commands.VersionIncrementType.Major,
            new UserId(userId)
        );
        _aggregateContext.Aggregate = aggregate;
    }

    [Then(
        @"A document with name ""([^""]*)"", filnemae ""([^""]*)"" extension ""([^""]*)"" is created by ""([^""]*)"""
    )]
    public void ThenADocumentWithNameFilnemaeExtensionIsCreatedBy(
        string name,
        string file,
        string ext,
        string userId
    )
    {
        var documentAggregate = _aggregateContext.Aggregate as Document;
        Assert.NotNull(documentAggregate);
        Assert.Equal(file, documentAggregate.FileNameWIthoutExtension);
        Assert.Equal(name, documentAggregate.Name);
        Assert.Equal(ext, documentAggregate.Extension);
        Assert.Equal(new Version(1, 0), documentAggregate.DocumentVersion);

        var events = _aggregateContext.GetEvents();
        Assert.Single(events);
        var @event = events.LastOrDefault() as DocumentCreated;

        Assert.NotNull(@event);
        Assert.Equal(name, @event.Name);
        Assert.Equal(ext, @event.Extension);
        Assert.Equal(file, @event.FileNameWithoutExtension);
        Assert.Equal(userId, @event.UserId);
        Assert.Equal(new Version(1, 0), @event.Version);
        Assert.Equal(documentAggregate.Key, @event.Key);
    }

    [Given(
        @"An existing document with key ""([^""]*)"", name ""([^""]*)"", filename ""([^""]*)"" and extension ""([^""]*)"""
    )]
    public void GivenAnExistingDocumentWithKeyNameFileAndExtension(
        string key,
        string name,
        string file,
        string ext
    )
    {
        var aggregate = new Document();
        aggregate.CreateDocument(
            DocumentKey.Parse(key),
            name,
            file,
            ext,
            Management.Documents.Commands.VersionIncrementType.Major,
            new UserId("osef")
        );
        _aggregateContext.Aggregate = aggregate;
    }

    [When(@"A user delete the document")]
    public void WhenAUserDeleteTheDocument()
    {
        var documentAggregate = _aggregateContext.Aggregate as Document;

        Assert.NotNull(documentAggregate);

        _aggregateContext.Result = documentAggregate.Delete(new UserId("osef2"));
    }

    [Then(@"The document is deleted")]
    public void ThenTheDocumentIsDeleted()
    {
        var documentAggregate = _aggregateContext.Aggregate as Document;

        Assert.NotNull(documentAggregate);
        Assert.True(documentAggregate.Deleted);

        var events = _aggregateContext.GetEvents();
        Assert.Single(events);
        var @event = events.LastOrDefault() as DocumentDeleted;

        Assert.NotNull(@event);
        Assert.Equal(documentAggregate.Id, @event.Id);
    }

    [When(
        @"A user with id ""([^""]*)"" modifies the document with new name ""([^""]*)"", new filename ""([^""]*)"", and new extension ""([^""]*)"""
    )]
    public void WhenAUserWithIdModifiesTheDocumentWithNewNameNewFilenameAndNewExtension(
        string testuser,
        string name,
        string filename,
        string extension
    )
    {
        var documentAggregate = _aggregateContext.Aggregate as Document;

        Assert.NotNull(documentAggregate);

        _aggregateContext.Result = documentAggregate.Modify(
            documentAggregate.Key,
            name,
            filename,
            extension,
            Management.Documents.Commands.VersionIncrementType.Major,
            new UserId(testuser)
        );
    }

    [Then(
        @"The document's name, filename, and extension are updated to ""([^""]*)"", ""([^""]*)"", and ""([^""]*)"" respectively"
    )]
    public void ThenTheDocumentsNameFilenameAndExtensionAreUpdatedToAndRespectively(
        string myUpdatedDoc,
        string updatedfile,
        string newext
    )
    {
        var documentAggregate = _aggregateContext.Aggregate as Document;

        Assert.NotNull(documentAggregate);
        Assert.Equal(myUpdatedDoc, documentAggregate.Name);
        Assert.Equal(updatedfile, documentAggregate.FileNameWIthoutExtension);
        Assert.Equal(newext, documentAggregate.Extension);

        var events = _aggregateContext.GetEvents();
        Assert.Single(events); // Assuming there are two events generated for the document modification

        var documentModifiedEvent = events.LastOrDefault() as DocumentModified;
        Assert.NotNull(documentModifiedEvent);
        Assert.Equal(myUpdatedDoc, documentModifiedEvent.Name);
        Assert.Equal(updatedfile, documentModifiedEvent.FileNameWithoutExtension);
        Assert.Equal(newext, documentModifiedEvent.Extension);
    }
}
