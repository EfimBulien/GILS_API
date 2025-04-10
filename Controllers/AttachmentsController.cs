using GilsApi.Models;
using GilsApi.Services;
using ApplicationDbContext = GilsApi.Data.ApplicationDbContext;

namespace GilsApi.Controllers;

public class AttachmentsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Attachment>(context, cacheService);