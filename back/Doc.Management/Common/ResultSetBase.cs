using System.Collections.Generic;

namespace Doc.Management.Common
{
    public abstract class ResultSetBase<T>
    {
        protected ResultSetBase(
            IReadOnlyList<T> data,
            long totalItemCount,
            bool hasNextPage,
            bool hasPreviousPage)
        {
            Data = data;
            TotalItemCount = totalItemCount;
            HasNextPage = hasNextPage;
            HasPreviousPage = hasPreviousPage;
        }

        public IReadOnlyList<T> Data { get; }
        public long TotalItemCount { get; }
        public bool HasNextPage { get; }
        public bool HasPreviousPage { get; }
    }
}
