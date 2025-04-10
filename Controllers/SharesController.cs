using GilsApi.Models;
using GilsApi.Services;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

public class SharesController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Share>(context, cacheService);