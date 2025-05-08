namespace GilsApi.Models;

public partial class SimilarTrack
{
    public string IdSimilarTrack { get; set; } = null!;

    public string TrackId1 { get; set; } = null!;

    public string TrackId2 { get; set; } = null!;

    public double SimilarScore { get; set; }

    public virtual Track TrackId1Navigation { get; set; } = null!;

    public virtual Track TrackId2Navigation { get; set; } = null!;
}
