using GilsApi.Models;
using GilsApi.Services;

namespace GilsApi.Controllers;

public class RolesController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<City>(context, cacheService);