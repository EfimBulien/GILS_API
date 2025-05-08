namespace GilsApi.Models;

public partial class Album
{
    public string IdAlbum { get; set; } = null!;

    public string ArtistId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Cover { get; set; } = null!;

    public string? VideoCover { get; set; }

    public DateOnly ReleaseDate { get; set; }

    public bool IsPopular { get; set; }

    public virtual Artist Artist { get; set; } = null!;

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual ICollection<Share> Shares { get; set; } = new List<Share>();

    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
}
