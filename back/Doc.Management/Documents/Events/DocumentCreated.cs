using Doc.Management.ValueObjects;

namespace Doc.Management.Documents.Events;

public sealed record DocumentCreated(DocumentKey Key, string FileNameWithoutExtension, string Extension, UserId UserId);
