using GilsApi.Models;
using GilsApi.Services;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

public class CommentsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Comment>(context, cacheService);