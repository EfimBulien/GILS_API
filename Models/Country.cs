namespace GilsApi.Models;

public partial class Country
{
    public string IdCountry { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
