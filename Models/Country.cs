namespace GilsApi.Models;

public partial class Country
{
    public Guid IdCountry { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
