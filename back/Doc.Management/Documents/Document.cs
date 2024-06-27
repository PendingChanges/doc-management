using Doc.Management.CQRS;
using Doc.Management.Documents.Events;
using Doc.Management.ValueObjects;
using System;

namespace Doc.Management.Documents;

public sealed class Document : Aggregate
{
    public DocumentKey Key { get; private set; }
    public string Name { get; private set; }

    public string FileNameWIthoutExtension { get; private set; }

    public string Extension { get; private set; }

    public bool Deleted { get; private set; }

    public Version DocumentVersion { get; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Document() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public AggregateResult Create(DocumentKey key, string name, string nameWithoutExtension, string extension, UserId ownerId)
    {
        var result = AggregateResult.Create();

        var id = EntityId.NewEntityId();

        var @event = new DocumentCreated(id, key, name, nameWithoutExtension, extension, ownerId, new Version(1, 0));

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
        SetId(new EntityId(@event.Id));

        Key = DocumentKey.Parse(@event.Key);
        Name = @event.Name;
        FileNameWIthoutExtension = @event.FileNameWithoutExtension;
        Extension = @event.Extension;
        Deleted = false;

        IncrementVersion();
    }

#pragma warning disable S1172 // Unused method parameters should be removed
    private void Apply(DocumentDeleted _)
#pragma warning restore S1172 // Unused method parameters should be removed
    {
        Deleted = true;
        IncrementVersion();
    }
}
