using System;
using Doc.Management.ValueObjects;

namespace Doc.Management.Documents.Events;

public sealed record DocumentCreated(
    string Id,
    string Key,
    string Name,
    string FileNameWithoutExtension,
    string Extension,
    string UserId,
    Version Version
);
