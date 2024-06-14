﻿using Doc.Management.Documents.DataModels;
using Doc.Management.Documents.Events;
using Marten;
using Marten.Events.Projections;

namespace Doc.Management.Marten.Documents;

public class DocumentProjection : EventProjection
{
    public DocumentDocument Create(DocumentCreated documentCreated)
        => new(documentCreated.Id, documentCreated.Name);

    public void Project(DocumentDeleted @event, IDocumentOperations ops)
        => ops.Delete<DocumentDocument>(@event.Id);
}
