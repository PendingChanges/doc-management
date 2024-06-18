using TechTalk.SpecFlow;
using System.Linq;
using Xunit;
using Doc.Management.Documents.Events;
using Doc.Management.Documents;
using Doc.Management.ValueObjects;
using System.Xml.Linq;

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
    public void GivenNoExistingDocument()
    {
        //Nothing to do here
    }

    [When(@"A user with id ""([^""]*)"" create a document with key ""([^""]*)"" name ""([^""]*)"" and extension ""([^""]*)""")]
    public void WhenAUserWithIdCreateADocumentWithKeyNameAndExtension(string userId, string key, string name, string ext)
    {
        var aggregate = new Document();
        _aggregateContext.Result = aggregate.Create(DocumentKey.Parse(key), name, ext, new UserId(userId));
        _aggregateContext.Aggregate = aggregate;
    }

    [Then(@"A document with name ""([^""]*)"", extension ""([^""]*)"" is created by ""([^""]*)""")]
    public void ThenADocumentWithNameExtensionIsCreatedBy(string name, string ext, string userId)
    {
        var documentAggregate = _aggregateContext.Aggregate as Document;
        Assert.NotNull(documentAggregate);
        Assert.Equal(name, documentAggregate.NameWIthoutExtension);

        var events = _aggregateContext.GetEvents();
        Assert.Single(events);
        var @event = events.LastOrDefault() as DocumentCreated;

        Assert.NotNull(@event);
        Assert.Equal(name, @event.FileNameWithoutExtension);
        Assert.Equal(ext, @event.Extension);
        Assert.Equal(userId, @event.UserId);
        Assert.Equal(documentAggregate.Key, @event.Key);
    }


    [Given(@"An existing document with key ""([^""]*)""")]
    public void GivenAnExistingDocumentWithKey(string key)
    {
        throw new PendingStepException();
    }

    [Given(@"An existing document with key ""([^""]*)"", name ""([^""]*)"" and extension ""([^""]*)""")]
    public void GivenAnExistingDocumentWithKeyNameAndExtension(string key, string name, string ext)
    {
        var aggregate = new Document();
        aggregate.Create(DocumentKey.Parse(key), name, ext, new UserId("osef"));
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
        Assert.Equal(documentAggregate.Key, @event.Key);
    }
}
