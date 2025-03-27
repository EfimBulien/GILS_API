using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class SimilarTrack
{
    public int IdSimilarTrack { get; set; }

    public int TrackId1 { get; set; }

    public int TrackId2 { get; set; }

    public double SimilarScore { get; set; }

    public virtual Track TrackId1Navigation { get; set; } = null!;

    public virtual Track TrackId2Navigation { get; set; } = null!;
}
