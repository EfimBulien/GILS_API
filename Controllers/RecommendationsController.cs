using GilsApi.Models;
using GilsApi.Services;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

public class RecommendationsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Recommendation>(context, cacheService);