using GilsApi.Models;
using GilsApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace GilsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController(ApplicationDBContext context, IRedisCacheService cacheService) : ControllerBase
{
    private const string CacheKeyAll = "countries";
    private const string CacheKeyPrefix = "country:";

    // GET: api/countries
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
    {
        var cachedData = await cacheService.GetCacheAsync(CacheKeyAll);
        if (!string.IsNullOrEmpty(cachedData))
        {
            var cachedCountries = JsonSerializer.Deserialize<List<Country>>(cachedData);
            return Ok(new { source = "cache", data = cachedCountries });
        }

        var countries = await context.Countries.ToListAsync();
        await cacheService.SetCacheAsync(CacheKeyAll, countries, TimeSpan.FromMinutes(10));

        return Ok(new { source = "database", data = countries });
    }

    // GET: api/countries/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Country>> GetCountry(int id)
    {
        var cacheKey = $"{CacheKeyPrefix}{id}";

        var cachedData = await cacheService.GetCacheAsync(cacheKey);
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

        await cacheService.SetCacheAsync(cacheKey, country, TimeSpan.FromMinutes(10));
        return Ok(new { source = "database", data = country });
    }

    // POST: api/countries
    [HttpPost]
    public async Task<ActionResult<Country>> PostCountry(Country country)
    {
        context.Countries.Add(country);
        await context.SaveChangesAsync();

        await cacheService.RemoveCacheAsync(CacheKeyAll);

        return CreatedAtAction(nameof(GetCountry), new { id = country.IdCountry }, country);
    }

    // PUT: api/countries/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutCountry(int id, Country country)
    {
        if (id != country.IdCountry)
        {
            return BadRequest();
        }

        context.Entry(country).State = EntityState.Modified;
        await context.SaveChangesAsync();
        
        var cacheKey = $"{CacheKeyPrefix}{id}";
        await cacheService.SetCacheAsync(cacheKey, country, TimeSpan.FromMinutes(10));

        await cacheService.RemoveCacheAsync(CacheKeyAll);

        return NoContent();
    }

    // DELETE: api/countries/5
    [HttpDelete("{id:int}")]
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
        await cacheService.RemoveCacheAsync(cacheKey);
        await cacheService.RemoveCacheAsync(CacheKeyAll);

        return NoContent();
    }
}
