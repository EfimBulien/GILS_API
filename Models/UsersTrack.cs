namespace GilsApi.Models;

public partial class UsersTrack
{
    public Guid IdUserTrack { get; set; }

    public Guid UserId { get; set; }

    public Guid TrackId { get; set; }

    public virtual Track Track { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
