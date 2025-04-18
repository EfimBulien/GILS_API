using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GilsApi.DTO;
using GilsApi.Models;
using GilsApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ApplicationDbContext = GilsApi.Models.ApplicationDbContext;

namespace GilsApi.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(ApplicationDbContext context, IRedisCacheService cacheService, IConfiguration config) 
    : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto userDto)
    {
        if (await context.Users.AnyAsync(u => u.Email == userDto.Email))
        {
            return BadRequest("Пользователь с таким email уже существует.");
        }

        var user = new User
        {
            IdUser = Guid.NewGuid().ToString(),
            Email = userDto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Phone = userDto.Phone,
            Birthday = userDto.Birthday,
            CityId = userDto.CityId.ToString(),
            RoleId = userDto.RoleId.ToString()
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();
        
        return Ok("Регистрация успешна.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto request)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        {
            return Unauthorized("Неверный email или пароль.");
        }

        var token = GenerateJwtToken(user);
        await cacheService.SetCacheAsync($"token:{user.IdUser}", token, TimeSpan.FromHours(1));

        return Ok(new { token });
    }

    private string GenerateJwtToken(User user)
    {
        var jwtKey = config["Jwt:Key"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.IdUser),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.RoleId)
        };

        var token = new JwtSecurityToken(
            issuer: "GilsApi",
            audience: "GilsApiClients",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
