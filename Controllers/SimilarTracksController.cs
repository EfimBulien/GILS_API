using GilsApi.Models;
using GilsApi.Services;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

public class SimilarTracksController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<SimilarTrack>(context, cacheService);