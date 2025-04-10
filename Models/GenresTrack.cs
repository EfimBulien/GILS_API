namespace GilsApi.Models;

public partial class GenresTrack
{
    public Guid IdGenreTrack { get; set; }

    public Guid GenreId { get; set; }

    public Guid TrackId { get; set; }

    public virtual Genre Genre { get; set; } = null!;

    public virtual Track Track { get; set; } = null!;
}
