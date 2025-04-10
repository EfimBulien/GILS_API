using GilsApi.Models;
using GilsApi.Services;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

public class MarksController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Mark>(context, cacheService);