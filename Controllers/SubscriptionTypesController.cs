using GilsApi.Models;
using GilsApi.Services;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

public class SubscriptionTypesController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<SubscriptionType>(context, cacheService);