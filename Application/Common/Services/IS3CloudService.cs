namespace Application.Common.Services;

public interface IS3CloudService
{
    Task UploadFileAsync(
        string bucketName,
        string fileName,
        Stream stream,
        CancellationToken cancellationToken);

    Task DeleteFileAsync(string bucketName, string fileName, CancellationToken cancellationToken);

    Task DeleteFileByUrlAsync(
        string bucketName,
        string fileUrl,
        CancellationToken cancellationToken);

    Task<Stream?> GetFileAsync(string bucketName, string fileName, CancellationToken cancellationToken);

    Task<IEnumerable<string>> GetFilesWithPrefixAsync(string bucketName, string prefix,
        CancellationToken cancellationToken);
}
