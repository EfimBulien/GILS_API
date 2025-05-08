namespace GilsApi.Models;

public partial class UsersTrack
{
    public string IdUserTrack { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string TrackId { get; set; } = null!;

    public virtual Track Track { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
