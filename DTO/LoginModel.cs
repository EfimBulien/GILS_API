namespace GilsApi.DTO;

public class LoginModel(string email, string password)
{
    public required string Email { get; init; } = email;
    public required string Password { get; init; } = password;
}