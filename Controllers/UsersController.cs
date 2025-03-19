using GilsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GilsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(ApplicationDBContext context) : ControllerBase
{
    // GET: api/users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await context.Users.ToListAsync();
    }

    // GET: api/users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return user;
    }

    // POST: api/users
    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUser), new { id = user.IdUser }, user);
    }

    // PUT: api/users/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, User user)
    {
        if (id != user.IdUser)
        {
            return BadRequest();
        }
        context.Entry(user).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/users/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        context.Users.Remove(user);
        await context.SaveChangesAsync();
        return NoContent();
    }
}