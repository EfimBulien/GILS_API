using GilsApi.Data;
using GilsApi.Models;
using GilsApi.Services;

namespace GilsApi.Controllers;

public class LyricsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Lyric>(context, cacheService);