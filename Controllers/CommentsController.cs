using GilsApi.Models;
using GilsApi.Services;

namespace GilsApi.Controllers;

public class CommentsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Comment>(context, cacheService);