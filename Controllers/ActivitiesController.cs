using GilsApi.Models;
using GilsApi.Services;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

public class ActivitiesController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Activity>(context, cacheService);