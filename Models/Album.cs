namespace GilsApi.Models;

public partial class Album
{
    public Guid IdAlbum { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Cover { get; set; } = null!;

    public string? VideoCover { get; set; }

    public DateOnly ReleaseDate { get; set; }

    public bool IsPopular { get; set; }

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual ICollection<Share> Shares { get; set; } = new List<Share>();

    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();

    public virtual User User { get; set; } = null!;
}
