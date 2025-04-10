using GilsApi.Models;
using GilsApi.Services;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

public class AlbumsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Album>(context, cacheService);