using Amazon.S3;
using Amazon.S3.Model;
using Doc.Management.Documents;
using Microsoft.Extensions.Options;

namespace Doc.Management.S3;
public class S3Store : IStoreFile
{
    private readonly IAmazonS3 _s3Client;
    private readonly S3Options _s3Options;

    public S3Store(IAmazonS3 s3Client, IOptionsSnapshot<S3Options> s3OptionsSnapshot)
    {
        _s3Client = s3Client;
        _s3Options = s3OptionsSnapshot.Value;
    }

    public async Task UploadStreamAsync(Stream stream, string key, CancellationToken cancellationToken = default)
    {
        var request = new PutObjectRequest
        {
            InputStream = stream,
            Key = key,
            BucketName = _s3Options.BucketName
        };

        await _s3Client.PutObjectAsync(request);
    }

    public Task<Stream> GetStreamAsync(string key, CancellationToken cancellationToker = default)
    {
        return _s3Client.GetObjectStreamAsync(_s3Options.BucketName, key, new Dictionary<string, object>());
    }
}
