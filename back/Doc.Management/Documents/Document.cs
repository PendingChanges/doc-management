using Doc.Management.CQRS;
using Doc.Management.Documents.Events;
using Doc.Management.ValueObjects;

namespace Doc.Management.Documents;

public sealed class Document : Aggregate
{
    public string Name { get; private set; }
    public bool Deleted { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Document() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public AggregateResult Create(string name, UserId ownerId)
    {
        var result = AggregateResult.Create();

        var id = EntityId.NewEntityId();

        var @event = new DocumentCreated(id, name, ownerId);

        Apply(@event);
        result.AddEvent(@event);

        return result;
    }

    public AggregateResult Delete(UserId userId)
    {
        var result = AggregateResult.Create();

        var @event = new DocumentDeleted(Id, userId);
        Apply(@event);
        result.AddEvent(@event);

        return result;
    }

    private void Apply(DocumentCreated @event)
    {
        SetId(@event.Id);

        Name = @event.Name;
        Deleted = false;

        IncrementVersion();
    }

    private void Apply(DocumentDeleted @event)
    {
        Deleted = true;
        IncrementVersion();
    }
}
