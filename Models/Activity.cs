namespace GilsApi.Models;

public partial class Activity
{
    public Guid IdActivity { get; set; }

    public Guid UserId { get; set; }

    public Guid TrackId { get; set; }

    public Guid ActionId { get; set; }

    public DateTime? Timestamp { get; set; }

    public virtual Action Action { get; set; } = null!;

    public virtual Track Track { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
