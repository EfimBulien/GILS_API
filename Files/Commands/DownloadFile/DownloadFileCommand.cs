using MediatR;

namespace GilsApi.Files.Commands.DownloadFile;

public sealed record DownloadFileCommand(
    string FileName,
    string BucketName
) : IRequest<FileResponse>;