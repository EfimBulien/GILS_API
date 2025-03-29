using GilsApi.Data;
using GilsApi.Models;
using GilsApi.Services;

namespace GilsApi.Controllers;

public class ReasonsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Reason>(context, cacheService);