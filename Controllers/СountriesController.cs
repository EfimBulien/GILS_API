using GilsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Text.Json;

namespace GilsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController(ApplicationDBContext context, IConnectionMultiplexer redis) : ControllerBase
{
    private readonly IDatabase _cache = redis.GetDatabase();
    
    private const string CacheKeyAll = "countries_all";
    private const string CacheKeyPrefix = "country_";

    // GET: api/countries
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
    {
        string? cachedData = await _cache.StringGetAsync(CacheKeyAll);
        if (!string.IsNullOrEmpty(cachedData))
        {
            var cachedCountries = JsonSerializer.Deserialize<List<Country>>(cachedData);
            return Ok(new { source = "cache", data = cachedCountries });
        }

        var countries = await context.Countries.ToListAsync();
        await _cache.StringSetAsync(CacheKeyAll, JsonSerializer.Serialize(countries), TimeSpan.FromMinutes(10));

        return Ok(new { source = "database", data = countries });
    }

    // GET: api/countries/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Country>> GetCountry(int id)
    {
        var cacheKey = $"{CacheKeyPrefix}{id}";

        string? cachedData = await _cache.StringGetAsync(cacheKey);
        
        if (!string.IsNullOrEmpty(cachedData))
        {
            var cachedCountry = JsonSerializer.Deserialize<Country>(cachedData);
            return Ok(new { source = "cache", data = cachedCountry });
        }

        var country = await context.Countries.FindAsync(id);
        if (country == null)
        {
            return NotFound();
        }

        await _cache.StringSetAsync(cacheKey, JsonSerializer.Serialize(country), TimeSpan.FromMinutes(10));
        return Ok(new { source = "database", data = country });
    }

    // POST: api/countries
    [HttpPost]
    public async Task<ActionResult<Country>> PostCountry(Country country)
    {
        context.Countries.Add(country);
        await context.SaveChangesAsync();

        await _cache.KeyDeleteAsync(CacheKeyAll);

        return CreatedAtAction(nameof(GetCountry), new { id = country.IdCountry }, country);
    }

    // PUT: api/countries/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCountry(int id, Country country)
    {
        if (id != country.IdCountry)
        {
            return BadRequest();
        }

        context.Entry(country).State = EntityState.Modified;
        await context.SaveChangesAsync();
        
        var cacheKey = $"{CacheKeyPrefix}{id}";
        await _cache.StringSetAsync(cacheKey, JsonSerializer.Serialize(country), TimeSpan.FromMinutes(10));

        await _cache.KeyDeleteAsync(CacheKeyAll);

        return NoContent();
    }

    // DELETE: api/countries/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        var country = await context.Countries.FindAsync(id);
        if (country == null)
        {
            return NotFound();
        }

        context.Countries.Remove(country);
        
        await context.SaveChangesAsync();
        
        var cacheKey = $"{CacheKeyPrefix}{id}";
        await _cache.KeyDeleteAsync(cacheKey);
        await _cache.KeyDeleteAsync(CacheKeyAll);
        
        return NoContent();
    }
}