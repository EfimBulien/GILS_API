using GilsApi.Data;
using GilsApi.Models;
using GilsApi.Services;

namespace GilsApi.Controllers;

public class AttachmentsController(ApplicationDbContext context, IRedisCacheService cacheService) 
    : BaseController<Attachment>(context, cacheService);