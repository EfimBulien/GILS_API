using GilsApi.Models;
using GilsApi.Services;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

public class PodcastsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Podcast>(context, cacheService);