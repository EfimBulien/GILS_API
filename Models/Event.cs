namespace GilsApi.Models;

public partial class Event
{
    public string IdEvent { get; set; } = null!;

    public string ArtistId { get; set; } = null!;

    public DateOnly Date { get; set; }

    public string Place { get; set; } = null!;

    public string CountryId { get; set; } = null!;

    public virtual Artist Artist { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;
}
