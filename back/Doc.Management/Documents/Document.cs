using Doc.Management.CQRS;
using Doc.Management.Documents.Events;
using Doc.Management.ValueObjects;

namespace Doc.Management.Documents;

public sealed class Document : Aggregate
{
    public DocumentKey Key { get; private set; }

    public string NameWIthoutExtension { get; private set; }

    public string Extension { get; private set; }

    public bool Deleted { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Document() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public AggregateResult Create(DocumentKey key, string nameWithoutExtension, string extension, UserId ownerId)
    {
        var result = AggregateResult.Create();

        var @event = new DocumentCreated(key, nameWithoutExtension, extension, ownerId);

        Apply(@event);
        result.AddEvent(@event);

        return result;
    }

    public AggregateResult Delete(UserId userId)
    {
        var result = AggregateResult.Create();

        var @event = new DocumentDeleted(Key, userId);
        Apply(@event);
        result.AddEvent(@event);

        return result;
    }

    private void Apply(DocumentCreated @event)
    {
        Key = @event.Key;
        NameWIthoutExtension = @event.FileNameWithoutExtension;
        Extension = @event.Extension;
        Deleted = false;

        IncrementVersion();
    }

    private void Apply(DocumentDeleted @event)
    {
        Deleted = true;
        IncrementVersion();
    }
}
