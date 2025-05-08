using MediatR;

namespace GilsApi.Files.Commands.UploadFile;

public sealed record UploadFileCommand(
    Stream Stream,
    string FileName,
    string FileType,
    long FileLength
) : IRequest<string>;
