using GilsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GilsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController(ApplicationDBContext context) : ControllerBase
{
    // GET: api/roles
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
    {
        return await context.Roles.ToListAsync();
    }

    // GET: api/roles/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Role>> GetRole(int id)
    {
        var role = await context.Roles.FindAsync(id);
        if (role == null)
        {
            return NotFound();
        }
        return role;
    }

    // POST: api/roles
    [HttpPost]
    public async Task<ActionResult<Role>> PostRole(Role role)
    {
        context.Roles.Add(role);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetRole), new { id = role.IdRole }, role);
    }

    // PUT: api/roles/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRole(int id, Role role)
    {
        if (id != role.IdRole)
        {
            return BadRequest();
        }
        context.Entry(role).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/roles/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(int id)
    {
        var role = await context.Roles.FindAsync(id);
        if (role == null)
        {
            return NotFound();
        }
        context.Roles.Remove(role);
        await context.SaveChangesAsync();
        return NoContent();
    }
}