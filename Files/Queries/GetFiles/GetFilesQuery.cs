using MediatR;

namespace GilsApi.Files.Queries.GetFiles;

public sealed record GetFilesQuery(
    string BucketName
) : IRequest<List<string>>;
