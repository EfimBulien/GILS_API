using GilsApi.Data;
using GilsApi.Models;
using GilsApi.Services;

namespace GilsApi.Controllers;

public class SamplesController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Sample>(context, cacheService);