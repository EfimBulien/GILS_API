namespace GilsApi.Models;

public partial class SimilarArtist
{
    public string IdSimilarArtist { get; set; } = null!;

    public string ArtistId1 { get; set; } = null!;

    public string ArtistId2 { get; set; } = null!;

    public double SimilarScore { get; set; }

    public virtual User ArtistId1Navigation { get; set; } = null!;

    public virtual User ArtistId2Navigation { get; set; } = null!;
}
