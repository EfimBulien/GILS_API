using GilsApi.Models;
using GilsApi.Services;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

public class EventsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Event>(context, cacheService);