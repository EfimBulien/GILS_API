using GilsApi.Data;
using GilsApi.Models;
using GilsApi.Services;

namespace GilsApi.Controllers;

public class ActivitiesController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Activity>(context, cacheService);