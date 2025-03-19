using GilsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GilsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CitiesController(ApplicationDBContext context) : ControllerBase
{
    // GET: api/cities
    [HttpGet]
    public async Task<ActionResult<IEnumerable<City>>> GetCities()
    {
        return await context.Cities.ToListAsync();
    }

    // GET: api/cities/5
    [HttpGet("{id}")]
    public async Task<ActionResult<City>> GetCity(int id)
    {
        var city = await context.Cities.FindAsync(id);
        if (city == null)
        {
            return NotFound();
        }
        return city;
    }

    // POST: api/cities
    [HttpPost]
    public async Task<ActionResult<City>> PostCity(City city)
    {
        context.Cities.Add(city);
        await context.SaveChangesAsync();
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
        return NoContent();
    }

    // DELETE: api/cities/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCity(int id)
    {
        var city = await context.Cities.FindAsync(id);
        if (city == null)
        {
            return NotFound();
        }
        context.Cities.Remove(city);
        await context.SaveChangesAsync();
        return NoContent();
    }
}