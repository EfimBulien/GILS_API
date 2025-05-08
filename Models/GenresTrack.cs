namespace GilsApi.Models;

public partial class GenresTrack
{
    public string IdGenreTrack { get; set; } = null!;

    public string GenreId { get; set; } = null!;

    public string TrackId { get; set; } = null!;

    public virtual Genre Genre { get; set; } = null!;

    public virtual Track Track { get; set; } = null!;
}
