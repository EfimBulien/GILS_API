using GilsApi.Models;
using GilsApi.Services;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

public class GenresController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Genre>(context, cacheService);