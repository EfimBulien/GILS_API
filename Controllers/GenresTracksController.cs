using GilsApi.Models;
using GilsApi.Services;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

public class GenresTracksController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<GenresTrack>(context, cacheService);