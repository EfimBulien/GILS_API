using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GilsApi.Data;
using GilsApi.DTO;
using GilsApi.Models;
using GilsApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GilsApi.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(ApplicationDbContext context, IRedisCacheService cacheService) : ControllerBase
{
    private const string SecretKey = "b3BlbnNlc2FtZWFuZG1vcmVzZWNyZXRrZXkh"; 

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto userDto)
    {
        if (await context.Users.AnyAsync(u => u.Email == userDto.Email))
        {
            return BadRequest("Пользователь с таким email уже существует.");
        }

        var user = new User
        {
            Email = userDto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Phone = userDto.Phone,
            Birthday = userDto.Birthday,
            CityId = userDto.CityId,
            RoleId = userDto.RoleId
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

    private static string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.RoleId.ToString())
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
