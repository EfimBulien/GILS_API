using GilsApi.Data;
using GilsApi.Models;
using GilsApi.Services;

namespace GilsApi.Controllers;

public class ReactionsUsersPostsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<ReactionsUsersPost>(context, cacheService);