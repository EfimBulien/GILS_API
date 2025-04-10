using GilsApi.Models;
using GilsApi.Services;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

public class TracksController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Track>(context, cacheService);