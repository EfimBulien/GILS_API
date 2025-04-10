namespace GilsApi.Models;

public partial class Event
{
    public Guid IdEvent { get; set; }

    public Guid UserId { get; set; }

    public DateOnly Date { get; set; }

    public string Place { get; set; } = null!;

    public Guid CountryId { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
