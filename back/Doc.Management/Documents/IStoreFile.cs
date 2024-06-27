using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Doc.Management.Documents;
public interface IStoreFile
{
    Task UploadStreamAsync(Stream stream, string key, CancellationToken cancellationToken = default);

    Task<Stream> GetStreamAsync(string key, CancellationToken cancellationToken = default);
}
