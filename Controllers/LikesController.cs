using GilsApi.Models;
using GilsApi.Services;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

public class LikesController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Like>(context, cacheService);