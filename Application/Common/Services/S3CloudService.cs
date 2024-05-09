using Amazon.S3.Model;
using Amazon.S3;
using Application.Common.Options;
using Microsoft.Extensions.Options;

namespace Application.Common.Services;

public class S3CloudService : IS3CloudService
{
    private readonly S3CloudOptions _options;

    public S3CloudService(IOptionsSnapshot<S3CloudOptions> optionsSnapshot)
    {
        _options = optionsSnapshot.Value;
    }

    public async Task UploadFileAsync(
        string bucketName,
        string fileName,
        Stream stream,
        CancellationToken cancellationToken)
    {
        var config = new AmazonS3Config
        {
            ServiceURL = _options.ServiceUrl
        };

        using var client = new AmazonS3Client(
            _options.AccessKey,
            _options.SecretKey,
            config);

        var putObjectRequest = new PutObjectRequest
        {
            InputStream = stream,
            BucketName = bucketName,
            Key = fileName,
            CannedACL = S3CannedACL.PublicRead
        };

        await client.PutObjectAsync(putObjectRequest, cancellationToken);
    }

    public async Task DeleteFileByUrlAsync(
        string bucketName,
        string fileUrl,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(fileUrl))
        {
            return;
        }

        var uri = new Uri(fileUrl);
        var filename = Path.GetFileName(uri.LocalPath);
        await DeleteFileAsync(bucketName, filename, cancellationToken);
    }

    public async Task DeleteFileAsync(string bucketName, string fileName, CancellationToken cancellationToken)
    {
        var config = new AmazonS3Config
        {
            ServiceURL = _options.ServiceUrl
        };

        using var client = new AmazonS3Client(
            _options.AccessKey,
            _options.SecretKey,
            config);

        var deleteObjectRequest = new DeleteObjectRequest
        {
            BucketName = bucketName,
            Key = fileName,
        };

        await client.DeleteObjectAsync(deleteObjectRequest, cancellationToken);
    }

    public async Task<Stream?> GetFileAsync(string bucketName, string fileName, CancellationToken cancellationToken)
    {
        var config = new AmazonS3Config
        {
            ServiceURL = _options.ServiceUrl
        };

        using var client = new AmazonS3Client(
            _options.AccessKey,
            _options.SecretKey,
            config);

        var getObjectRequest = new GetObjectRequest
        {
            BucketName = bucketName,
            Key = fileName
        };

        try
        {
            var objectInfo = await client.GetObjectAsync(getObjectRequest, cancellationToken);
            return objectInfo.ResponseStream;
        }
        catch (AmazonS3Exception)
        {
            return null;
        }
    }

    public async Task<IEnumerable<string>> GetFilesWithPrefixAsync(string bucketName, string prefix,
        CancellationToken cancellationToken)
    {
        var config = new AmazonS3Config
        {
            ServiceURL = _options.ServiceUrl
        };

        using var client = new AmazonS3Client(
            _options.AccessKey,
            _options.SecretKey,
            config);

        var paginator = client.Paginators.ListObjectsV2(new ListObjectsV2Request
        {
            BucketName = bucketName,
            Prefix = prefix,
        });

        var objects = new List<S3Object>();
        await foreach (var response in paginator.Responses.WithCancellation(cancellationToken))
        {
            objects.AddRange(response.S3Objects);
        }

        return objects.Select(result => result.Key);
    }
}
