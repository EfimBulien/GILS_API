using GilsApi.Models;
using GilsApi.Services;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

public class CitiesController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<City>(context, cacheService);