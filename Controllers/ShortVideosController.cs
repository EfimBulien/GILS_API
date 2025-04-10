using GilsApi.Models;
using GilsApi.Services;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

public class ShortVideosController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<ShortVideo>(context, cacheService);