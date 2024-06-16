namespace Doc.Management.S3;
public class S3Options
{
    public const string S3 = "S3";
    public string? ServiceUrl { get; set; }
    public string? AccessKey { get; set; }
    public string? SecretKey { get; set; }
    public string? BucketName { get; set; }
}
