using System;

namespace Doc.Management.GraphQL.Documents.Outputs;

public record Document(
    Guid Id,
    string Key,
    string Name,
    string FileNameWithoutExtension,
    string Extension,
    Version Version
);
