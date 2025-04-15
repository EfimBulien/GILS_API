using GilsApi.Models;
using GilsApi.Services;

namespace GilsApi.Controllers;

public class RecommendationsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Recommendation>(context, cacheService);