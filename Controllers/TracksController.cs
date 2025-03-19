using GilsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GilsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TracksController(ApplicationDBContext context) : ControllerBase
{
    // GET: api/tracks
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Track>>> GetTracks()
    {
        return await context.Tracks.ToListAsync();
    }

    // GET: api/tracks/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Track>> GetTrack(int id)
    {
        var track = await context.Tracks.FindAsync(id);
        if (track == null)
        {
            return NotFound();
        }
        return track;
    }

    // POST: api/tracks
    [HttpPost]
    public async Task<ActionResult<Track>> PostTrack(Track track)
    {
        context.Tracks.Add(track);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetTrack), new { id = track.IdTrack }, track);
    }

    // PUT: api/tracks/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTrack(int id, Track track)
    {
        if (id != track.IdTrack)
        {
            return BadRequest();
        }
        context.Entry(track).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/tracks/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTrack(int id)
    {
        var track = await context.Tracks.FindAsync(id);
        if (track == null)
        {
            return NotFound();
        }
        context.Tracks.Remove(track);
        await context.SaveChangesAsync();
        return NoContent();
    }
}