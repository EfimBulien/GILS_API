using GilsApi.Data;
using GilsApi.Models;
using GilsApi.Services;

namespace GilsApi.Controllers;

public class ShortVideosController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<ShortVideo>(context, cacheService);