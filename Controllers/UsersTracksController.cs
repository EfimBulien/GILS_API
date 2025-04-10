using GilsApi.Models;
using GilsApi.Services;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

public class UsersTracksController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<UsersTrack>(context, cacheService);