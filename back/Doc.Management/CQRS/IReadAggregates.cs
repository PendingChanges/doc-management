using System.Threading;
using System.Threading.Tasks;

namespace Doc.Management.CQRS
{
    public interface IReadAggregates
    {
        Task<T?> LoadAsync<T>(
            string id,
            int? version = null,
            CancellationToken cancellationToken = default
        )
            where T : Aggregate;
    }
}
