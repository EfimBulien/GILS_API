using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class Genre
{
    public int IdGenre { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<GenresTrack> GenresTracks { get; set; } = new List<GenresTrack>();
}
