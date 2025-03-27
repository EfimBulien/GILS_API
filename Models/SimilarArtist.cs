using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class SimilarArtist
{
    public int IdSimilarArtist { get; set; }

    public int ArtistId1 { get; set; }

    public int ArtistId2 { get; set; }

    public double SimilarScore { get; set; }

    public virtual User ArtistId1Navigation { get; set; } = null!;

    public virtual User ArtistId2Navigation { get; set; } = null!;
}
