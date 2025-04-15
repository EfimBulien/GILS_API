using GilsApi.Models;
using GilsApi.Services;

namespace GilsApi.Controllers;

public class CountriesController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Country>(context, cacheService);