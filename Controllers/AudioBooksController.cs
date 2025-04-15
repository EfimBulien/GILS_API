using GilsApi.Models;
using GilsApi.Services;

namespace GilsApi.Controllers;

public class AudioBooksController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<AudioBook>(context, cacheService);