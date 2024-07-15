using System;

namespace Doc.Management.Documents.Events;

public sealed record DocumentModified(
    Guid Id,
    string Key,
    string Name,
    string FileNameWithoutExtension,
    string Extension,
    string UserId,
    Version Version
);
