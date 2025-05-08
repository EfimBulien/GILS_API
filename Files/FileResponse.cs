namespace GilsApi.Files;

public sealed record FileResponse(
    MemoryStream MemoryStream,
    string ContentType,
    string FileName
);
