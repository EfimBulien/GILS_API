namespace GilsApi.Models;

public partial class SimilarArtist
{
    public Guid IdSimilarArtist { get; set; }

    public Guid ArtistId1 { get; set; }

    public Guid ArtistId2 { get; set; }

    public double SimilarScore { get; set; }

    public virtual User ArtistId1Navigation { get; set; } = null!;

    public virtual User ArtistId2Navigation { get; set; } = null!;
}
