using GilsApi.Common.Interfaces;
using GilsApi.Domain.Entities;
using GilsApi.Exceptions;
using MediatR;

namespace GilsApi.Files.Commands.DownloadFile;

public class DownloadFileCommandHandler(
    IFileDataRepository fileDataRepository,
    IFileManager fileManager
) : IRequestHandler<DownloadFileCommand, FileResponse>
{
    public async Task<FileResponse> Handle(DownloadFileCommand request, CancellationToken cancellationToken)
    {
        var fileData = await fileDataRepository.FindFileDataByFileNameAndBucketName(
                           request.BucketName,
                           request.FileName,
                           cancellationToken)
                       ?? throw new NotFoundException(nameof(FileData), 
                           $"{request.FileName} in {request.BucketName}");

        var stream = await fileManager.DownloadFile(request.BucketName, request.FileName);
        return new FileResponse(stream, fileData.FileType, fileData.FileName);
    }
}
