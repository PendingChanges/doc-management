using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Doc.Management.Documents.DataModels;

namespace Doc.Management.GraphQL.Documents;

public static class DocumentsMapper
{
    public static IReadOnlyList<Outputs.Document> ToDocuments(
        this IReadOnlyList<DocumentDocument> clients
    ) => clients.Select(ToDocument).ToList();

    public static Outputs.Document ToDocument(this DocumentDocument clientDocument) =>
        new(
            clientDocument.Id,
            clientDocument.Key,
            clientDocument.Name,
            clientDocument.FileNameWithoutExtension,
            clientDocument.Extension,
            clientDocument.Version
        );
}
