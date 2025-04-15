using GilsApi.Models;
using GilsApi.Services;

namespace GilsApi.Controllers;

public class GenresController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Genre>(context, cacheService);