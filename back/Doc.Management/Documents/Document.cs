using System;
using Doc.Management.CQRS;
using Doc.Management.Documents.Commands;
using Doc.Management.Documents.Events;
using Doc.Management.ValueObjects;

namespace Doc.Management.Documents;

public sealed class Document : Aggregate
{
    public DocumentKey Key { get; private set; }
    public string Name { get; private set; }

    public string FileNameWIthoutExtension { get; private set; }

    public string Extension { get; private set; }

    public bool Deleted { get; private set; }

    public DocumentVersion DocumentVersion { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Document() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public AggregateResult CreateDocument(
        DocumentKey key,
        string name,
        string nameWithoutExtension,
        string extension,
        VersionIncrementType versionIncrementType,
        UserId ownerId
    )
    {
        var result = AggregateResult.Create();

        var id = Guid.NewGuid();

        var @event = new DocumentCreated(
            id,
            key,
            name,
            nameWithoutExtension,
            extension,
            ownerId,
            DocumentVersion.NewVersion(versionIncrementType)
        );

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

    public AggregateResult Modify(
        DocumentKey key,
        string name,
        string fileNameWithoutExtension,
        string extension,
        VersionIncrementType versionIncrementType,
        UserId ownerId
    )
    {
        var result = AggregateResult.Create();

        var @event = new DocumentModified(
            Id,
            key,
            name,
            fileNameWithoutExtension,
            extension,
            ownerId,
            DocumentVersion.Increment(versionIncrementType)
        );

        Apply(@event);
        result.AddEvent(@event);

        return result;
    }

    private void Apply(DocumentModified @event)
    {
        Key = new DocumentKey(@event.Key);
        Name = @event.Name;
        FileNameWIthoutExtension = @event.FileNameWithoutExtension;
        Extension = @event.Extension;
        DocumentVersion = new DocumentVersion(@event.Version);

        IncrementVersion();
    }

    private void Apply(DocumentCreated @event)
    {
        SetId(@event.Id);

        Key = new DocumentKey(@event.Key);
        Name = @event.Name;
        FileNameWIthoutExtension = @event.FileNameWithoutExtension;
        Extension = @event.Extension;
        Deleted = false;
        DocumentVersion = new DocumentVersion(@event.Version);

        IncrementVersion();
    }

#pragma warning disable S1172 // Unused method parameters should be removed
    private void Apply(DocumentDeleted @event)
#pragma warning restore S1172 // Unused method parameters should be removed
    {
        Deleted = true;
        IncrementVersion();
    }
}
