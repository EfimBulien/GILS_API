using GilsApi.Models;
using GilsApi.Services;

namespace GilsApi.Controllers;

public class UsersTracksController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<UsersTrack>(context, cacheService);