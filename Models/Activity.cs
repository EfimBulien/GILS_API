namespace GilsApi.Models;

public partial class Activity
{
    public string IdActivity { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string TrackId { get; set; } = null!;

    public string ActionId { get; set; } = null!;

    public DateTime? Timestamp { get; set; }

    public virtual Action Action { get; set; } = null!;

    public virtual Track Track { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
