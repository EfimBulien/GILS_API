namespace GilsApi.Models;

public partial class City
{
    public string IdCity { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string CountryId { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
