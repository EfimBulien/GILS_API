using GilsApi.Data;
using GilsApi.Models;
using GilsApi.Services;

namespace GilsApi.Controllers;

public class TracksController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Track>(context, cacheService);