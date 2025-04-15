using GilsApi.Models;
using GilsApi.Services;
namespace GilsApi.Controllers;

public class SimilarTracksController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<SimilarTrack>(context, cacheService);