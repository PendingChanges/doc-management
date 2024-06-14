using Doc.Management.Common;

namespace Doc.Management.Documents.DataModels
{
    public class GetDocumentsRequest : PaginatedRequestBase
    {
        public GetDocumentsRequest(
            int? skip,
            int? take,
            string? sortBy,
            string? sortDirection,
            string userId)
            : base(
            skip ?? Constants.DefaultPageNumber,
            take ?? Constants.DefaultPageSize,
            sortBy ?? Constants.DefaultDocumentSortBy,
            sortDirection ?? Constants.DefaultSortDirection,
            userId)
        {
        }
    }
}
