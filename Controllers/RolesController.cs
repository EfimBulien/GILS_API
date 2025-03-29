using GilsApi.Models;
using GilsApi.Services;
using GilsApi.Data;

namespace GilsApi.Controllers;

public class RolesController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<City>(context, cacheService);