using GilsApi.Common.Interfaces;
using GilsApi.Domain.Entities;
using GilsApi.Exceptions;
using MediatR;

namespace GilsApi.Files.Commands.DeleteFile;

public class DeleteFileCommandHandler(
    IFileDataRepository fileDataRepository,
    IFileManager fileManager
) : IRequestHandler<DeleteFileCommand, Unit>
{
    public async Task<Unit> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
    {
        var fileData = await fileDataRepository.FindFileDataByFileNameAndBucketName(
                           request.BucketName, 
                           request.FileName, 
                           cancellationToken)
                       ?? throw new NotFoundException(nameof(FileData),
                           $"{request.FileName} in {request.BucketName}");

        await fileManager.DeleteFile(request.BucketName, request.FileName);
        await fileDataRepository.DeleteFileData(fileData, cancellationToken);
        return await Unit.Task;
    }
}
