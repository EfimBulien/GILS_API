using GilsApi.Models;
using GilsApi.Services;
using Action = GilsApi.Models.Action;

namespace GilsApi.Controllers;

public class ActionsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Action>(context, cacheService);