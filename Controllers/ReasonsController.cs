using GilsApi.Models;
using GilsApi.Services;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

public class ReasonsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Reason>(context, cacheService);