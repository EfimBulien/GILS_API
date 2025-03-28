using System.Text.Json;
using GilsApi.Data;
using GilsApi.Models;
using GilsApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GilsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CitiesController(ApplicationDbContext context, IRedisCacheService cacheService)  : ControllerBase
{
    
    private const string CacheKeyAll = "cities";
    private const string CacheKeyPrefix = "city:";
    
    // GET: api/cities
    [HttpGet]
    public async Task<ActionResult<IEnumerable<City>>> GetCities()
    {
        var cachedData = await cacheService.GetCacheAsync(CacheKeyAll);
        if (!string.IsNullOrEmpty(cachedData))
        {
            var cachedCities = JsonSerializer.Deserialize<List<City>>(cachedData);
            return Ok(new { source = "cache", data = cachedCities });
        }

        var cities = await context.Cities.ToListAsync();
        await cacheService.SetCacheAsync(CacheKeyAll, cities, TimeSpan.FromMinutes(10));

        return Ok(new { source = "database", data = cities });
    }

    // GET: api/cities/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<City>> GetCity(int id)
    {
        var cacheKey = $"{CacheKeyPrefix}{id}";
        var cachedData = await cacheService.GetCacheAsync(cacheKey);

        if (!string.IsNullOrEmpty(cachedData))
        {
            var cachedCities = JsonSerializer.Deserialize<City>(cachedData);
            return Ok(new { source = "cache", data = cachedCities });
        }

        var city = await context.Cities.FindAsync(id);
        if (city == null)
        {
            return NotFound();
        }

        await cacheService.SetCacheAsync(cacheKey, city, TimeSpan.FromMinutes(10));
        return Ok(new { source = "database", data = city });
    }

    // POST: api/cities
    [HttpPost]
    public async Task<ActionResult<City>> PostCity(City city)
    {
        context.Cities.Add(city);
        await context.SaveChangesAsync();
        
        await cacheService.RemoveCacheAsync(CacheKeyAll);
        return CreatedAtAction(nameof(GetCity), new { id = city.IdCity }, city);
    }

    // PUT: api/cities/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCity(int id, City city)
    {
        if (id != city.IdCity)
        {
            return BadRequest();
        }
        context.Entry(city).State = EntityState.Modified;
        await context.SaveChangesAsync();
        
        var cacheKey = $"{CacheKeyPrefix}{id}";
        await cacheService.SetCacheAsync(cacheKey, city, TimeSpan.FromMinutes(10));
        await cacheService.RemoveCacheAsync(CacheKeyAll);
        
        return NoContent();
    }

    // DELETE: api/cities/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCity(int id)
    {
        var city = await context.Cities.FindAsync(id);
        if (city == null)
        {
            return NotFound();
        }
        context.Cities.Remove(city);
        await context.SaveChangesAsync();
        
        var cacheKey = $"{CacheKeyPrefix}{id}";
        await cacheService.RemoveCacheAsync(cacheKey);
        await cacheService.RemoveCacheAsync(CacheKeyAll);
        
        return NoContent();
    }
}