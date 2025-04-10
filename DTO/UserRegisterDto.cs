namespace GilsApi.DTO;

public class UserRegisterDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public decimal Phone { get; set; }
    public required DateOnly Birthday { get; set; }
    public required Guid CityId { get; set; }
    public required Guid RoleId { get; set; }
}