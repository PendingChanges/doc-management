using Doc.Management.ValueObjects;

namespace Doc.Management.Documents.Events;

public sealed record DocumentModified(EntityId Id, string Name, UserId UserId);
