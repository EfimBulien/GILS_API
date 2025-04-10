using GilsApi.Models;
using GilsApi.Services;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

public class PostsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Post>(context, cacheService);