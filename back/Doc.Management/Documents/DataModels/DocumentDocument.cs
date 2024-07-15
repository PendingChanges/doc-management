using System;

namespace Doc.Management.Documents.DataModels;

public record DocumentDocument(
    Guid Id,
    string Key,
    string Name,
    string FileNameWithoutExtension,
    string Extension,
    Version Version
);
