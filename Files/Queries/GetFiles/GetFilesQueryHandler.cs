using GilsApi.Common.Interfaces;
using MediatR;

namespace GilsApi.Files.Queries.GetFiles;

public class GetFilesQueryHandler(
    IFileDataRepository fileDataRepository
) : IRequestHandler<GetFilesQuery, List<string>>
{
    public async Task<List<string>> Handle(GetFilesQuery request, CancellationToken cancellationToken)
    {
       return await fileDataRepository.FindNamesByBucket(request.BucketName, cancellationToken);
    }
}
