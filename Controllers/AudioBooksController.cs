using GilsApi.Models;
using GilsApi.Services;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

public class AudioBooksController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<AudioBook>(context, cacheService);