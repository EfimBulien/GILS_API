using GilsApi.Models;
using GilsApi.Services;

namespace GilsApi.Controllers;

public class ArtistsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Artist>(context, cacheService);