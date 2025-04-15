using System.Text.Json;
using GilsApi.Common;
using GilsApi.Models;
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TEntity>>> GetAll()
    {
        var cachedData = await cacheService.GetCacheAsync(_cacheKeyAll);
        if (!string.IsNullOrEmpty(cachedData))
        {
            var cachedEntities = JsonSerializer.Deserialize<List<TEntity>>(cachedData);
            return Ok(new
            {
                source = Constants.ResponseSources.Cache,
                data = cachedEntities
            });
        }

        var entities = await _dbSet.ToListAsync();
        await cacheService.SetCacheAsync(_cacheKeyAll, entities, Constants.StandardCacheDuration);

        return Ok(new
        {
            source = Constants.ResponseSources.Database,
            data = entities
        });
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TEntity>> GetById(Guid id)
    {
        var cacheKey = $"{_cacheKeyPrefix}{id}";

        var cachedData = await cacheService.GetCacheAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedData))
        {
            var cachedEntity = JsonSerializer.Deserialize<TEntity>(cachedData);
            return Ok(new
            {
                source = Constants.ResponseSources.Cache,
                data = cachedEntity
            });
        }

        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
        {
            return NotFound();
        }

        await cacheService.SetCacheAsync(cacheKey, entity, Constants.StandardCacheDuration);
        return Ok(new
        {
            source = Constants.ResponseSources.Database,
            data = entity
        });
    }

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

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, TEntity entity)
    {
        if (!EntityIdMatches(id, entity))
        {
            return BadRequest();
        }

        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync();

        var cacheKey = $"{_cacheKeyPrefix}{id}";
        await cacheService.SetCacheAsync(cacheKey, entity, Constants.StandardCacheDuration);
        await cacheService.RemoveCacheAsync(_cacheKeyAll);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
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

    private static Guid GetEntityId(TEntity entity)
    {
        var prop = typeof(TEntity)
            .GetProperty("IdEntity") ?? typeof(TEntity)
            .GetProperty("Id");

        return prop != null && Guid.TryParse(prop.GetValue(entity)?
            .ToString(), out var id) ? id : Guid.Empty;
    }

    private static bool EntityIdMatches(Guid id, TEntity entity)
    {
        return GetEntityId(entity) == id;
    }
}