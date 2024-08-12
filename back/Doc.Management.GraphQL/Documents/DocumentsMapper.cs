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

    public static Outputs.Document ToDocument(this DocumentDocument documentDocument) =>
        new(
            documentDocument.Id,
            documentDocument.Name,
            documentDocument.FileNameWithoutExtension,
            documentDocument.Extension,
            documentDocument.Version
        );

    public static Outputs.Document? ToDocumentOrNull(this DocumentDocument? documentDocument) =>
        documentDocument == null ? null : ToDocument(documentDocument);
}
