using GilsApi.Data;
using GilsApi.Models;
using GilsApi.Services;

namespace GilsApi.Controllers;

public class MarksController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Mark>(context, cacheService);