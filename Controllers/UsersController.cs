using System.Security.Claims;
using System.Text.Json;
using GilsApi.Common;
using GilsApi.Models;
using GilsApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController(ApplicationDbContext context, IRedisCacheService cacheService)
    : ControllerBase
{
    private const string CacheKeyAll = "users_all";
    private const string CacheKeyPrefix = "user:";
    private const string AdminRoleName = "Admin";
    
    // GET: api/users
    [HttpGet]
    [Authorize(Roles = AdminRoleName)]
    public async Task<ActionResult<IEnumerable<object>>> GetAllUsers()
    {
        var cachedData = await cacheService.GetCacheAsync(CacheKeyAll);
        if (!string.IsNullOrEmpty(cachedData))
        {
            var cachedUsers = JsonSerializer.Deserialize<List<object>>(cachedData);
            return Ok(new
            {
                source = Constants.ResponseSources.Cache,
                data = cachedUsers
            });
        }

        var users = await context.Users.ToListAsync();
        var safeUsers = users.Select(GetSafeUser).ToList();

        await cacheService
            .SetCacheAsync(CacheKeyAll, safeUsers, Constants.StandardCacheDuration);
        return Ok(new
        {
            source = Constants.ResponseSources.Database, 
            data = (List<object>?)safeUsers
        });
    }

    // GET: api/users/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<object>> GetUser(Guid id)
    {
        if (!IsAuthorizedUser(id))
        {
            return Forbid();
        }

        var cacheKey = $"{CacheKeyPrefix}{id}";
            
        var cachedData = await cacheService.GetCacheAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedData))
        {
            var cachedUser = JsonSerializer.Deserialize<object>(cachedData);
            return Ok(new
            {
                source = Constants.ResponseSources.Cache,
                data = cachedUser
            });
        }

        var user = await context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var safeUser = GetSafeUser(user);
        await cacheService
            .SetCacheAsync(cacheKey, safeUser, Constants.StandardCacheDuration);
            
        return Ok(new
        {
            source = Constants.ResponseSources.Database,  
            data = safeUser
        });
    }

    // PUT: api/users/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> PutUser(Guid id, [FromBody] User user)
    {
        if (!IsAuthorizedUser(id))
        {
            return Forbid();
        }

        var existingUser = await context.Users.FindAsync(id);
        if (existingUser == null)
        {
            return NotFound();
        }

        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        existingUser.Phone = user.Phone;
        existingUser.Birthday = user.Birthday;
        existingUser.CityId = user.CityId;

        context.Entry(existingUser).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
            var cacheKey = $"{CacheKeyPrefix}{id}";
            await cacheService
                .SetCacheAsync(cacheKey, GetSafeUser(existingUser), Constants.StandardCacheDuration);
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

    // DELETE: api/users/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        if (!IsAuthorizedUser(id))
        {
            return Forbid();
        }

        var user = await context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        context.Users.Remove(user);
        await context.SaveChangesAsync();

        var cacheKey = $"{CacheKeyPrefix}{id}";
        await cacheService.RemoveCacheAsync(cacheKey);
        await cacheService.RemoveCacheAsync(CacheKeyAll);
        return NoContent();
    }
    
    private bool IsAuthorizedUser(Guid id)
    {
        var currentUserIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(currentUserIdString, out var currentUserId))
        {
            currentUserId = Guid.Empty;
        }

        return currentUserId == id || User.IsInRole(AdminRoleName);
    }

    private static object GetSafeUser(User user) 
        => new {
            user.IdUser,
            user.Email,
            user.FirstName,
            user.LastName,
            user.RoleId,
            user.Phone,
            user.Birthday,
            user.CityId
        };
}
