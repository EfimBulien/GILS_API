using Application.Files.Commands.DeleteFile;
using Application.Files.Commands.DownloadFile;
using Application.Files.Commands.UploadFile;
using Application.Files.Queries.GetFiles;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using GilsApi.Services;

namespace GilsApi.Controllers;

[ApiController]
[Route("api/file")]
public class FileController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly IConfiguration _configuration;
    private readonly IRedisCacheService _cacheService;
    private readonly string _cacheKeyAll = "files_all";

    public FileController(ISender mediator, IConfiguration configuration, IRedisCacheService cacheService)
    {
        _mediator = mediator;
        _configuration = configuration;
        _cacheService = cacheService;
    }

    [HttpPost("upload/{bucketName}")]
    public async Task<ActionResult> Upload(string bucketName, IFormFile file)
    {
        if (file.ContentType is not ("audio/mpeg" or "video/mp4"))
            return BadRequest("Only mp3 and mp4 files are allowed.");

        if (file.Length > 524288000) // 500 MB
            return BadRequest("File size exceeds the limit of 500 MB.");

        try
        {
            var stream = file.OpenReadStream();
            var result = await _mediator.Send(new UploadFileCommand(stream, file.FileName, file.ContentType, file.Length));
            await _cacheService.RemoveCacheAsync(_cacheKeyAll);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("download/{bucketName}/{fileName}")]
    public async Task<ActionResult> Download(string bucketName, string fileName)
    {
        try
        {
            var command = new DownloadFileCommand(bucketName, fileName);
            var result = await _mediator.Send(command);
            return File(result.MemoryStream, result.ContentType, result.FileName);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{bucketName}")]
    public async Task<ActionResult<List<string>>> GetAll(string bucketName)
    {
        var cachedData = await _cacheService.GetCacheAsync(_cacheKeyAll);
        if (!string.IsNullOrEmpty(cachedData))
        {
            var cachedEntities = JsonSerializer.Deserialize<List<string>>(cachedData);
            return Ok(new
            {
                source = "Cache",
                data = cachedEntities
            });
        }

        try
        {
            var result = await _mediator.Send(new GetFilesQuery(bucketName));
            await _cacheService.SetCacheAsync(_cacheKeyAll, result, TimeSpan.FromMinutes(10));
            return Ok(new
            {
                source = "Database",
                data = result
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("delete/{bucketName}/{fileName}")]
    public async Task<ActionResult> Delete(string bucketName, string fileName)
    {
        try
        {
            await _mediator.Send(new DeleteFileCommand(bucketName, fileName));
            await _cacheService.RemoveCacheAsync(_cacheKeyAll);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
