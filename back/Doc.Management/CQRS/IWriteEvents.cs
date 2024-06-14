using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Doc.Management.CQRS
{
    public interface IWriteEvents
    {
        Task StoreAsync(string aggregateId, long version, IEnumerable<object> events, CancellationToken ct = default);
    }
}