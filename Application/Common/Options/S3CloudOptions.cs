namespace Application.Common.Options;

public class S3CloudOptions
{
    public string ServiceUrl { get; set; } = string.Empty;

    public string AccessKey { get; set; } = string.Empty;

    public string SecretKey { get; set; } = string.Empty;

    public string Const { get; set; } = string.Empty;

    public string ConstUrlForNews { get; set; } = string.Empty;

    public string BucketName { get; set; } = string.Empty;

    public string BucketNameForNews { get; set; } = string.Empty;
}
