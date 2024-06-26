﻿namespace Doc.Management.Common
{
    public abstract class PaginatedRequestBase
    {
        protected PaginatedRequestBase(
            int skip,
            int take,
            string sortBy,
            string sortDirection)
        {
            Skip = skip;
            Take = take;
            SortBy = sortBy;
            SortDirection = sortDirection;
        }

        public int Skip { get; }
        public int Take { get; }
        public string SortBy { get; }
        public string SortDirection { get; }
    }
}
