using GilsApi.Data;
using GilsApi.Models;
using GilsApi.Services;

namespace GilsApi.Controllers;

public class CitiesController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<City>(context, cacheService);