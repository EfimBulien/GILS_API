using GilsApi.Models;
using GilsApi.Services;

namespace GilsApi.Controllers;

public class SharesController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Share>(context, cacheService);