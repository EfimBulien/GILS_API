using GilsApi.Models;
using GilsApi.Services;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

public class DurationsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Duration>(context, cacheService);