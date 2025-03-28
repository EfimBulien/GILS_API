using GilsApi.Data;
using GilsApi.Models;
using GilsApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace GilsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubscriptionTypesController(ApplicationDbContext context, IRedisCacheService cacheService) : ControllerBase
{
    private const string CacheKeyAll = "subscriptiontypes";
    private const string CacheKeyPrefix = "subscriptiontype:";
    
    // GET: api/subscriptiontypes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SubscriptionType>>> GetSubscriptionTypes()
    {
        var cachedData = await cacheService.GetCacheAsync(CacheKeyAll);
        if (!string.IsNullOrEmpty(cachedData))
        {
            var cachedTypes = JsonSerializer.Deserialize<List<SubscriptionType>>(cachedData);
            return Ok(new { source = "cache", data = cachedTypes });
        }

        var subscriptionTypes = await context.SubscriptionTypes.ToListAsync();
        await cacheService.SetCacheAsync(CacheKeyAll, subscriptionTypes, TimeSpan.FromMinutes(10));

        return Ok(new { source = "database", data = subscriptionTypes });
    }

    // GET: api/subscriptiontypes/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<SubscriptionType>> GetSubscriptionType(int id)
    {
        var cacheKey = $"{CacheKeyPrefix}{id}";
        var cachedData = await cacheService.GetCacheAsync(cacheKey);

        if (!string.IsNullOrEmpty(cachedData))
        {
            var cachedTypes = JsonSerializer.Deserialize<List<SubscriptionType>>(cachedData);
            return Ok(new { source = "cache", data = cachedTypes });
        }
        
        var subscriptionType = await context.SubscriptionTypes.FindAsync(id);
        if (subscriptionType == null)
        {
            return NotFound();
        }
        
        return Ok(new { source = "database", data = subscriptionType });
    }

    // POST: api/subscriptiontypes
    [HttpPost]
    public async Task<ActionResult<SubscriptionType>> PostSubscriptionType(SubscriptionType subscriptionType)
    {
        
        context.SubscriptionTypes.Add(subscriptionType);
        await context.SaveChangesAsync();

        await cacheService.RemoveCacheAsync(CacheKeyAll);
        return CreatedAtAction(nameof(GetSubscriptionType), new
        {
            id = subscriptionType.IdSubscriptionType
        }, subscriptionType);
    }

    // PUT: api/subscriptiontypes/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutSubscriptionType(int id, SubscriptionType subscriptionType)
    {
        if (id != subscriptionType.IdSubscriptionType)
        {
            return BadRequest();
        }
        
        context.Entry(subscriptionType).State = EntityState.Modified;
        await context.SaveChangesAsync();
        
        var cacheKey = $"{CacheKeyPrefix}{id}";
        await cacheService.SetCacheAsync(cacheKey, subscriptionType, TimeSpan.FromMinutes(10));
        await cacheService.RemoveCacheAsync(CacheKeyAll);
        
        return NoContent();
    }

    // DELETE: api/subscriptiontypes/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteSubscriptionType(int id)
    {
        var subscriptionType = await context.SubscriptionTypes.FindAsync(id);
        if (subscriptionType == null)
        {
            return NotFound();
        }
        context.SubscriptionTypes.Remove(subscriptionType);
        await context.SaveChangesAsync();
        
        var cacheKey = $"{CacheKeyPrefix}{id}";
        await cacheService.RemoveCacheAsync(cacheKey);
        await cacheService.RemoveCacheAsync(CacheKeyAll);
        
        return NoContent();
    }
}