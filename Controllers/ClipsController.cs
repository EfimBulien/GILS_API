using GilsApi.Data;
using GilsApi.Models;
using GilsApi.Services;

namespace GilsApi.Controllers;

public class ClipsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Clip>(context, cacheService);