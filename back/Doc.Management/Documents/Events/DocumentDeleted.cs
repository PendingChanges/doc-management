using Doc.Management.ValueObjects;

namespace Doc.Management.Documents.Events;

public sealed record DocumentDeleted(string Id, string UserId);
