using GilsApi.Models;
using GilsApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using GilsApi.Data;

namespace GilsApi.Controllers;

[Route("api/[controller]")]
[ApiController]

// INFO НА ПРОДАКШЕНЕ РАЗКОММЕНТИРОВАТЬ!
//[Authorize]
public class UsersController(ApplicationDbContext context, IRedisCacheService cacheService) : ControllerBase
{
    private const string CacheKeyAll = "users";
    private const string CacheKeyPrefix = "user:";

    // GET: api/users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var cachedData = await cacheService.GetCacheAsync(CacheKeyAll);
        if (!string.IsNullOrEmpty(cachedData))
        {
            var cachedUsers = JsonSerializer.Deserialize<List<User>>(cachedData);
            return Ok(new { source = "cache", data = cachedUsers });
        }

        var users = await context.Users.ToListAsync();
        await cacheService.SetCacheAsync(CacheKeyAll, users, TimeSpan.FromMinutes(5));

        return Ok(new { source = "database", data = users });
    }

    // GET: api/users/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var cacheKey = $"{CacheKeyPrefix}{id}";
        var cachedData = await cacheService.GetCacheAsync(cacheKey);
        
        if (!string.IsNullOrEmpty(cachedData))
        {
            var cachedUser = JsonSerializer.Deserialize<User>(cachedData);
            return Ok(new { source = "cache", data = cachedUser });
        }

        var user = await context.Users.FindAsync(id);
        if (user == null) return NotFound();

        await cacheService.SetCacheAsync(cacheKey, user, TimeSpan.FromMinutes(10));
        return Ok(new { source = "database", data = user });
    }

    // POST: api/users
    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user)
    {
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        context.Users.Add(user);
        await context.SaveChangesAsync();

        await cacheService.RemoveCacheAsync(CacheKeyAll);
        return CreatedAtAction(nameof(GetUser), new { id = user.IdUser }, user);
    }
    
    // PUT: api/users/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutUser(int id, User user)
    {
        if (id != user.IdUser) return BadRequest();
        
        context.Entry(user).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();

            var cacheKey = $"{CacheKeyPrefix}{id}";
            await cacheService.SetCacheAsync(cacheKey, user, TimeSpan.FromMinutes(10));
            await cacheService.RemoveCacheAsync(CacheKeyAll);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!context.Users.Any(u => u.IdUser == id))
            {
                return NotFound();
            }
            throw;
        }
        return NoContent();
    }
    
    // DELETE: api/users/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null) return NotFound();

        context.Users.Remove(user);
        await context.SaveChangesAsync();

        var cacheKey = $"{CacheKeyPrefix}{id}";
        await cacheService.RemoveCacheAsync(cacheKey);
        await cacheService.RemoveCacheAsync(CacheKeyAll);

        return NoContent();
    }
}
