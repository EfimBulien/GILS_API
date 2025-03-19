using GilsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GilsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubscriptionsController(ApplicationDBContext context) : ControllerBase
{
    // GET: api/subscriptions
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Subscription>>> GetSubscriptions()
    {
        return await context.Subscriptions.ToListAsync();
    }

    // GET: api/subscriptions/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Subscription>> GetSubscription(int id)
    {
        var subscription = await context.Subscriptions.FindAsync(id);
        if (subscription == null)
        {
            return NotFound();
        }
        return subscription;
    }

    // POST: api/subscriptions
    [HttpPost]
    public async Task<ActionResult<Subscription>> PostSubscription(Subscription subscription)
    {
        context.Subscriptions.Add(subscription);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetSubscription), new { id = subscription.IdSubscription }, subscription);
    }

    // PUT: api/subscriptions/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSubscription(int id, Subscription subscription)
    {
        if (id != subscription.IdSubscription)
        {
            return BadRequest();
        }
        context.Entry(subscription).State = EntityState.Modified;
        await context.SaveChangesAsync();
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
        return NoContent();
    }
}