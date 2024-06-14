using Doc.Management.CQRS;

namespace Doc.Management.Documents.Commands;

public record CreateDocument(string Name) : ICommand;
