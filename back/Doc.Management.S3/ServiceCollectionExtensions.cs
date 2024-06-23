using Amazon.Runtime;
using Amazon.S3;
using Doc.Management.Documents;
using Microsoft.Extensions.DependencyInjection;

namespace Doc.Management.S3;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddS3(this IServiceCollection services, S3Options s3Options)
    {
        var credentials = new BasicAWSCredentials(s3Options.AccessKey, s3Options.SecretKey);

        var client = new AmazonS3Client(credentials, new AmazonS3Config
        {
            ServiceURL = s3Options.ServiceUrl,
        });

        services.AddSingleton<IAmazonS3>((sp) => client);

        services.AddTransient<IStoreFile, S3Store>();

        return services;
    }
}
