using GilsApi.Models;
using GilsApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace GilsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController(ApplicationDBContext context, IRedisCacheService cacheService) : ControllerBase
{
    private const string CacheKeyAll = "roles";
    private const string CacheKeyPrefix = "role:";

    // GET: api/roles
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
    {
        var cachedData = await cacheService.GetCacheAsync(CacheKeyAll);
        if (!string.IsNullOrEmpty(cachedData))
        {
            var cachedRoles = JsonSerializer.Deserialize<List<Role>>(cachedData);
            return Ok(new { source = "cache", data = cachedRoles });
        }

        var roles = await context.Roles.ToListAsync();
        await cacheService.SetCacheAsync(CacheKeyAll, roles, TimeSpan.FromMinutes(10));

        return Ok(new { source = "database", data = roles });
    }

    // GET: api/roles/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Role>> GetRole(int id)
    {
        var cacheKey = $"{CacheKeyPrefix}{id}";
        var cachedData = await cacheService.GetCacheAsync(cacheKey);

        if (!string.IsNullOrEmpty(cachedData))
        {
            var cachedRole = JsonSerializer.Deserialize<Role>(cachedData);
            return Ok(new { source = "cache", data = cachedRole });
        }

        var role = await context.Roles.FindAsync(id);
        if (role == null) return NotFound();

        await cacheService.SetCacheAsync(cacheKey, role, TimeSpan.FromMinutes(10));
        return Ok(new { source = "database", data = role });
    }

    // POST: api/roles
    [HttpPost]
    public async Task<ActionResult<Role>> PostRole(Role role)
    {
        context.Roles.Add(role);
        await context.SaveChangesAsync();

        await cacheService.RemoveCacheAsync(CacheKeyAll);
        return CreatedAtAction(nameof(GetRole), new { id = role.IdRole }, role);
    }

    // PUT: api/roles/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutRole(int id, Role role)
    {
        if (id != role.IdRole) return BadRequest();

        context.Entry(role).State = EntityState.Modified;
        await context.SaveChangesAsync();

        var cacheKey = $"{CacheKeyPrefix}{id}";
        await cacheService.SetCacheAsync(cacheKey, role, TimeSpan.FromMinutes(10));
        await cacheService.RemoveCacheAsync(CacheKeyAll);

        return NoContent();
    }

    // DELETE: api/roles/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteRole(int id)
    {
        var role = await context.Roles.FindAsync(id);
        if (role == null) return NotFound();

        context.Roles.Remove(role);
        await context.SaveChangesAsync();

        var cacheKey = $"{CacheKeyPrefix}{id}";
        await cacheService.RemoveCacheAsync(cacheKey);
        await cacheService.RemoveCacheAsync(CacheKeyAll);

        return NoContent();
    }
}
