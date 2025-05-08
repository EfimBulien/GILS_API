using GilsApi.Domain.Common;

namespace GilsApi.Domain.Entities;

public class FileData : BaseEntity
{
    public required string BucketName { get; init; }
    public required string FileName { get; init; }
    public required string FileType { get; init; }
    public required ulong FileSize { get; init; }
}
