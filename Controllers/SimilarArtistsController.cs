using GilsApi.Models;
using GilsApi.Services;

namespace GilsApi.Controllers;

public class SimilarArtistsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<SimilarArtist>(context, cacheService);