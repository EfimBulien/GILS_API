using GilsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GilsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubscriptionTypesController(ApplicationDBContext context) : ControllerBase
{
    // GET: api/subscriptiontypes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SubscriptionType>>> GetSubscriptionTypes()
    {
        return await context.SubscriptionTypes.ToListAsync();
    }

    // GET: api/subscriptiontypes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<SubscriptionType>> GetSubscriptionType(int id)
    {
        var subscriptionType = await context.SubscriptionTypes.FindAsync(id);
        if (subscriptionType == null)
        {
            return NotFound();
        }
        return subscriptionType;
    }

    // POST: api/subscriptiontypes
    [HttpPost]
    public async Task<ActionResult<SubscriptionType>> PostSubscriptionType(SubscriptionType subscriptionType)
    {
        context.SubscriptionTypes.Add(subscriptionType);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetSubscriptionType), new { id = subscriptionType.IdSubscriptionType }, subscriptionType);
    }

    // PUT: api/subscriptiontypes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSubscriptionType(int id, SubscriptionType subscriptionType)
    {
        if (id != subscriptionType.IdSubscriptionType)
        {
            return BadRequest();
        }
        context.Entry(subscriptionType).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/subscriptiontypes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubscriptionType(int id)
    {
        var subscriptionType = await context.SubscriptionTypes.FindAsync(id);
        if (subscriptionType == null)
        {
            return NotFound();
        }
        context.SubscriptionTypes.Remove(subscriptionType);
        await context.SaveChangesAsync();
        return NoContent();
    }
}