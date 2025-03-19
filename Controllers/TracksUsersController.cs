using GilsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GilsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TracksUsersController(ApplicationDBContext context) : ControllerBase
{
    // GET: api/tracksusers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TracksUser>>> GetTracksUsers()
    {
        return await context.TracksUsers.ToListAsync();
    }

    // GET: api/tracksusers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TracksUser>> GetTrackUser(int id)
    {
        var trackUser = await context.TracksUsers.FindAsync(id);
        if (trackUser == null)
        {
            return NotFound();
        }
        return trackUser;
    }

    // POST: api/tracksusers
    [HttpPost]
    public async Task<ActionResult<TracksUser>> PostTrackUser(TracksUser trackUser)
    {
        context.TracksUsers.Add(trackUser);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetTrackUser), new { id = trackUser.IdTrackUser }, trackUser);
    }

    // PUT: api/tracksusers/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTrackUser(int id, TracksUser trackUser)
    {
        if (id != trackUser.IdTrackUser)
        {
            return BadRequest();
        }
        context.Entry(trackUser).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/tracksusers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTrackUser(int id)
    {
        var trackUser = await context.TracksUsers.FindAsync(id);
        if (trackUser == null)
        {
            return NotFound();
        }
        context.TracksUsers.Remove(trackUser);
        await context.SaveChangesAsync();
        return NoContent();
    }
}