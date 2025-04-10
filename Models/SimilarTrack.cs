namespace GilsApi.Models;

public partial class SimilarTrack
{
    public Guid IdSimilarTrack { get; set; }

    public Guid TrackId1 { get; set; }

    public Guid TrackId2 { get; set; }

    public double SimilarScore { get; set; }

    public virtual Track TrackId1Navigation { get; set; } = null!;

    public virtual Track TrackId2Navigation { get; set; } = null!;
}
