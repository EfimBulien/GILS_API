using GilsApi.Models;
using GilsApi.Services;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

public class CountriesController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Country>(context, cacheService);