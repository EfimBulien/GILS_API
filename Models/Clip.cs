namespace GilsApi.Models;

public partial class Clip
{
    public Guid IdClip { get; set; }

    public Guid TrackId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Clip1 { get; set; } = null!;

    public virtual Track Track { get; set; } = null!;
}
