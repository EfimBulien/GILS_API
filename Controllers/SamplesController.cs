using GilsApi.Models;
using GilsApi.Services;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

public class SamplesController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Sample>(context, cacheService);