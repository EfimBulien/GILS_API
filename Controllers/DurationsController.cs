using GilsApi.Data;
using GilsApi.Models;
using GilsApi.Services;

namespace GilsApi.Controllers;

public class DurationsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Duration>(context, cacheService);