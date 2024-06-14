using Doc.Management.CQRS;
using Doc.Management.ValueObjects;

namespace Doc.Management.Documents.Commands;

public record DeleteDocument(EntityId Id) : ICommand;
