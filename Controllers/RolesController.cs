using GilsApi.Models;
using GilsApi.Services;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

public class RolesController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<City>(context, cacheService);