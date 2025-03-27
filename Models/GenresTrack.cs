using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class GenresTrack
{
    public int IdGenreTrack { get; set; }

    public int GenreId { get; set; }

    public int TrackId { get; set; }

    public virtual Genre Genre { get; set; } = null!;

    public virtual Track Track { get; set; } = null!;
}
