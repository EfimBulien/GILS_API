using GilsApi.Models;
using GilsApi.Services;

namespace GilsApi.Controllers;

public class PostsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Post>(context, cacheService);