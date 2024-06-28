using TechTalk.SpecFlow;
using System.Linq;
using Xunit;
using Doc.Management.Documents.Events;
using Doc.Management.Documents;
using Doc.Management.ValueObjects;
using System;

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

    [When(@"A user with id ""([^""]*)"" create a document with key ""([^""]*)"" name ""([^""]*)"", filename ""([^""]*)"" and extension ""([^""]*)""")]
    public void WhenAUserWithIdCreateADocumentWithKeyNameFilenameAndExtension(string userId, string key, string name, string file, string ext)
    {
        var aggregate = new Document();
        _aggregateContext.Result = aggregate.Create(DocumentKey.Parse(key), name, file, ext, new UserId(userId));
        _aggregateContext.Aggregate = aggregate;
    }

    [Then(@"A document with name ""([^""]*)"", filnemae ""([^""]*)"" extension ""([^""]*)"" is created by ""([^""]*)""")]
    public void ThenADocumentWithNameFilnemaeExtensionIsCreatedBy(string name, string file, string ext, string userId)
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

    [Given(@"An existing document with key ""([^""]*)"", name ""([^""]*)"", file ""([^""]*)"" and extension ""([^""]*)""")]
    public void GivenAnExistingDocumentWithKeyNameFileAndExtension(string key, string name, string file, string ext)
    {
        var aggregate = new Document();
        aggregate.Create(DocumentKey.Parse(key), name, file, ext, new UserId("osef"));
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
}
