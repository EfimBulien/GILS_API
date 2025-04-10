namespace GilsApi.Models;

public partial class City
{
    public Guid IdCity { get; set; }

    public string Name { get; set; } = null!;

    public Guid CountryId { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
