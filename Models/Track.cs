namespace GilsApi.Models;

public partial class Track
{
    public Guid IdTrack { get; set; }

    public Guid AlbumId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Producer { get; set; }

    public string? MainArtist { get; set; }

    public string Track1 { get; set; } = null!;

    public int Duration { get; set; }

    public DateOnly ReleaseDate { get; set; }

    public bool IsPopular { get; set; }

    public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();

    public virtual Album Album { get; set; } = null!;

    public virtual ICollection<Clip> Clips { get; set; } = new List<Clip>();

    public virtual ICollection<GenresTrack> GenresTracks { get; set; } = new List<GenresTrack>();

    public virtual ICollection<Lyric> Lyrics { get; set; } = new List<Lyric>();

    public virtual ICollection<Recommendation> Recommendations { get; set; } = new List<Recommendation>();

    public virtual ICollection<SimilarTrack> SimilarTrackTrackId1Navigations { get; set; } = new List<SimilarTrack>();

    public virtual ICollection<SimilarTrack> SimilarTrackTrackId2Navigations { get; set; } = new List<SimilarTrack>();

    public virtual ICollection<UsersTrack> UsersTracks { get; set; } = new List<UsersTrack>();
}
