using GilsApi.Models;
using GilsApi.Services;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

public class ClipsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Clip>(context, cacheService);