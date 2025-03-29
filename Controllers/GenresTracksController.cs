using GilsApi.Data;
using GilsApi.Models;
using GilsApi.Services;

namespace GilsApi.Controllers;

public class GenresTracksController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<GenresTrack>(context, cacheService);