using GilsApi.Models;
using GilsApi.Services;

namespace GilsApi.Controllers;

public class SubscriptionsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Subscription>(context, cacheService);