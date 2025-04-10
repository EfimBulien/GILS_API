using GilsApi.Models;
using GilsApi.Services;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

public class SubscriptionsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Subscription>(context, cacheService);