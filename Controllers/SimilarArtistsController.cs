using GilsApi.Models;
using GilsApi.Services;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

public class SimilarArtistsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<SimilarArtist>(context, cacheService);