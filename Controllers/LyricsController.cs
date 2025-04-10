using GilsApi.Models;
using GilsApi.Services;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

public class LyricsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Lyric>(context, cacheService);