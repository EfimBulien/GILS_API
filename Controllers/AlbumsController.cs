using GilsApi.Models;
using GilsApi.Services;

namespace GilsApi.Controllers;

public class AlbumsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Album>(context, cacheService);