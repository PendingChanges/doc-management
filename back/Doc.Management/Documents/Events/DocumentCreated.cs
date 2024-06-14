using Doc.Management.ValueObjects;

namespace Doc.Management.Documents.Events;

public sealed record DocumentCreated(EntityId Id, string Name, UserId UserId);
