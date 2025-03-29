using System.Text.Json;
using GilsApi.Data;
using GilsApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GilsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class BaseController<TEntity>(ApplicationDbContext context, IRedisCacheService cacheService) 
    : ControllerBase where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
    private readonly string _cacheKeyAll = $"{typeof(TEntity).Name.ToLower()}_all";
    private readonly string _cacheKeyPrefix = $"{typeof(TEntity).Name.ToLower()}:";
    private readonly TimeSpan _standardCacheTimeSpan = TimeSpan.FromMinutes(10);
    private const string DatabaseSource = "database";
    private const string CacheSource = "database";

    // GET: api/bananas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TEntity>>> GetAll()
    {
        var cachedData = await cacheService.GetCacheAsync(_cacheKeyAll);
        if (!string.IsNullOrEmpty(cachedData))
        {
            var cachedEntities = JsonSerializer.Deserialize<List<TEntity>>(cachedData);
            return Ok(new
            {
                source = CacheSource, 
                data = cachedEntities
            });
        }

        var entities = await _dbSet.ToListAsync();
        await cacheService.SetCacheAsync(_cacheKeyAll, entities, _standardCacheTimeSpan);

        return Ok(new
        {
            source = DatabaseSource, 
            data = entities
        });
    }

    // GET: api/bananas/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TEntity>> GetById(int id)
    {
        var cacheKey = $"{_cacheKeyPrefix}{id}";
            
        var cachedData = await cacheService.GetCacheAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedData))
        {
            var cachedEntity = JsonSerializer.Deserialize<TEntity>(cachedData);
            return Ok(new
            {
                source = DatabaseSource, 
                data = cachedEntity
            });
        }

        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
        {
            return NotFound();
        }

        await cacheService.SetCacheAsync(cacheKey, entity, _standardCacheTimeSpan);
        return Ok(new
        {
            source = DatabaseSource,
            data = entity
        });
    }

    // POST: api/banana
    [HttpPost]
    public async Task<ActionResult<TEntity>> Create(TEntity entity)
    {
        _dbSet.Add(entity);
        await context.SaveChangesAsync();
        await cacheService.RemoveCacheAsync(_cacheKeyAll);

        return CreatedAtAction(nameof(GetById), new
        {
            id = GetEntityId(entity)
        }, entity);
    }

    // PUT: api/banana/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, TEntity entity)
    {
        if (!EntityIdMatches(id, entity))
        {
            return BadRequest();
        }

        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync();

        var cacheKey = $"{_cacheKeyPrefix}{id}";
        await cacheService.SetCacheAsync(cacheKey, entity, _standardCacheTimeSpan);
        await cacheService.RemoveCacheAsync(_cacheKeyAll);

        return NoContent();
    }

    // DELETE: api/banana/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
        {
            return NotFound();
        }

        _dbSet.Remove(entity);
        await context.SaveChangesAsync();

        var cacheKey = $"{_cacheKeyPrefix}{id}";
        await cacheService.RemoveCacheAsync(cacheKey);
        await cacheService.RemoveCacheAsync(_cacheKeyAll);

        return NoContent();
    }
        
    private static int GetEntityId(TEntity entity)
    {
        var prop = typeof(TEntity)
            .GetProperty("IdEntity") ?? typeof(TEntity)
            .GetProperty("Id");
        return (int)(prop?.GetValue(entity) ?? 0);
    }

    private static bool EntityIdMatches(int id, TEntity entity)
    {
        return GetEntityId(entity) == id;
    }
}