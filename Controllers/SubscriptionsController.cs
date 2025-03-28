using System.Text.Json;
using GilsApi.Data;
using GilsApi.Models;
using GilsApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GilsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubscriptionsController(ApplicationDbContext context, IRedisCacheService cacheService) : ControllerBase
{
    private const string CacheKeyAll = "subscriptions";
    private const string CacheKeyPrefix = "subscription:";
    
    // GET: api/subscriptions
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Subscription>>> GetSubscriptions()
    {
        
        var cachedData = await cacheService.GetCacheAsync(CacheKeyAll);
        if (!string.IsNullOrEmpty(cachedData))
        {
            var cachedSubscriptions= JsonSerializer.Deserialize<List<Subscription>>(cachedData);
            return Ok(new { source = "cache", data = cachedSubscriptions });
        }

        var subscriptions = await context.SubscriptionTypes.ToListAsync();
        await cacheService.SetCacheAsync(CacheKeyAll, subscriptions, TimeSpan.FromMinutes(10));

        return Ok(new { source = "database", data = subscriptions });
    }

    // GET: api/subscriptions/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Subscription>> GetSubscription(int id)
    {
        var cacheKey = $"{CacheKeyPrefix}{id}";
        var cachedData = await cacheService.GetCacheAsync(cacheKey);

        if (!string.IsNullOrEmpty(cachedData))
        {
            var cachedSubscriptions = JsonSerializer.Deserialize<List<Subscription>>(cachedData);
            return Ok(new { source = "cache", data = cachedSubscriptions });
        }
        
        var subscription = await context.SubscriptionTypes.FindAsync(id);
        if (subscription == null)
        {
            return NotFound();
        }
        
        return Ok(new { source = "database", data = subscription });
    }

    // POST: api/subscriptions
    [HttpPost]
    public async Task<ActionResult<Subscription>> PostSubscription(Subscription subscription)
    {
        context.Subscriptions.Add(subscription);
        await context.SaveChangesAsync();
        await cacheService.RemoveCacheAsync(CacheKeyAll);
        return CreatedAtAction(nameof(GetSubscription), new
        {
            id = subscription.IdSubscription
        }, subscription);
    }

    // PUT: api/subscriptions/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutSubscription(int id, Subscription subscription)
    {
        if (id != subscription.IdSubscription)
        {
            return BadRequest();
        }
        context.Entry(subscription).State = EntityState.Modified;
        await context.SaveChangesAsync();
        
        var cacheKey = $"{CacheKeyPrefix}{id}";
        await cacheService.SetCacheAsync(cacheKey, subscription, TimeSpan.FromMinutes(10));
        await cacheService.RemoveCacheAsync(CacheKeyAll);
        
        return NoContent();
    }

    // DELETE: api/subscriptions/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubscription(int id)
    {
        var subscription = await context.Subscriptions.FindAsync(id);
        if (subscription == null)
        {
            return NotFound();
        }
        context.Subscriptions.Remove(subscription);
        await context.SaveChangesAsync();
        
        var cacheKey = $"{CacheKeyPrefix}{id}";
        await cacheService.RemoveCacheAsync(cacheKey);
        await cacheService.RemoveCacheAsync(CacheKeyAll);
        
        return NoContent();
    }
}