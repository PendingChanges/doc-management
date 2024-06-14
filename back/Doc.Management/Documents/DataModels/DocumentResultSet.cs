using Doc.Management.Common;
using System.Collections.Generic;

namespace Doc.Management.Documents.DataModels;

public class DocumentResultSet : ResultSetBase<DocumentDocument>
{
    public DocumentResultSet(
        IReadOnlyList<DocumentDocument> data,
        long totalItemCount,
        bool hasNextPage,
        bool hasPreviousPage)
        : base(
            data,
            totalItemCount,
            hasNextPage,
            hasPreviousPage)
    {
    }
}
