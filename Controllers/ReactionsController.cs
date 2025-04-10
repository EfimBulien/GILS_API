using GilsApi.Models;
using GilsApi.Services;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

public class ReactionsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Reaction>(context, cacheService);