using GilsApi.Models;
using GilsApi.Services;

namespace GilsApi.Controllers;

public class SubscriptionTypesController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<SubscriptionType>(context, cacheService);