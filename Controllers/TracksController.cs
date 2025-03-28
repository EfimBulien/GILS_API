using GilsApi.Data;
using GilsApi.Models;
using GilsApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace GilsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TracksController(ApplicationDbContext context, IRedisCacheService cacheService) : ControllerBase
{
    private const string CacheKeyAll = "tracks";
    private const string CacheKeyPrefix = "track:";

    // GET: api/tracks
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Track>>> GetTracks()
    {
        var cachedData = await cacheService.GetCacheAsync(CacheKeyAll);
        if (!string.IsNullOrEmpty(cachedData))
        {
            var cachedTracks = JsonSerializer.Deserialize<List<Track>>(cachedData);
            return Ok(new { source = "cache", data = cachedTracks });
        }

        var tracks = await context.Tracks.ToListAsync();
        await cacheService.SetCacheAsync(CacheKeyAll, tracks, TimeSpan.FromMinutes(10));

        return Ok(new { source = "database", data = tracks });
    }

    // GET: api/tracks/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Track>> GetTrack(int id)
    {
        var cacheKey = $"{CacheKeyPrefix}{id}";
        var cachedData = await cacheService.GetCacheAsync(cacheKey);

        if (!string.IsNullOrEmpty(cachedData))
        {
            var cachedTrack = JsonSerializer.Deserialize<Track>(cachedData);
            return Ok(new { source = "cache", data = cachedTrack });
        }

        var track = await context.Tracks.FindAsync(id);
        if (track == null)
        {
            return NotFound();
        }

        await cacheService.SetCacheAsync(cacheKey, track, TimeSpan.FromMinutes(10));
        return Ok(new { source = "database", data = track });
    }

    // POST: api/tracks
    [HttpPost]
    public async Task<ActionResult<Track>> PostTrack(Track track)
    {
        context.Tracks.Add(track);
        await context.SaveChangesAsync();

        await cacheService.RemoveCacheAsync(CacheKeyAll);
        return CreatedAtAction(nameof(GetTrack), new { id = track.IdTrack }, track);
    }

    // PUT: api/tracks/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutTrack(int id, Track track)
    {
        if (id != track.IdTrack)
        {
            return BadRequest();
        }

        context.Entry(track).State = EntityState.Modified;
        await context.SaveChangesAsync();

        var cacheKey = $"{CacheKeyPrefix}{id}";
        await cacheService.SetCacheAsync(cacheKey, track, TimeSpan.FromMinutes(10));
        await cacheService.RemoveCacheAsync(CacheKeyAll);

        return NoContent();
    }

    // DELETE: api/tracks/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTrack(int id)
    {
        var track = await context.Tracks.FindAsync(id);
        if (track == null)
        {
            return NotFound();
        }

        context.Tracks.Remove(track);
        await context.SaveChangesAsync();

        var cacheKey = $"{CacheKeyPrefix}{id}";
        await cacheService.RemoveCacheAsync(cacheKey);
        await cacheService.RemoveCacheAsync(CacheKeyAll);

        return NoContent();
    }
}
