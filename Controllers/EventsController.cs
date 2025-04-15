using GilsApi.Models;
using GilsApi.Services;

namespace GilsApi.Controllers;

public class EventsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Event>(context, cacheService);