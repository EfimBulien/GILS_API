using GilsApi.Models;
using GilsApi.Services;

namespace GilsApi.Controllers;

public class ReactionsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Reaction>(context, cacheService);