namespace GilsApi.Models;

public partial class Genre
{
    public string IdGenre { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<GenresTrack> GenresTracks { get; set; } = new List<GenresTrack>();
}
