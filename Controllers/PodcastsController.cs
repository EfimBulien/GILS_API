using GilsApi.Data;
using GilsApi.Models;
using GilsApi.Services;

namespace GilsApi.Controllers;

public class PodcastsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Podcast>(context, cacheService);