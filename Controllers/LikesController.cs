using GilsApi.Data;
using GilsApi.Models;
using GilsApi.Services;

namespace GilsApi.Controllers;

public class LikesController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Like>(context, cacheService);