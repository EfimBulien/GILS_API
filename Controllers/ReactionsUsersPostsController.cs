using GilsApi.Models;
using GilsApi.Services;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

public class ReactionsUsersPostsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<ReactionsUsersPost>(context, cacheService);