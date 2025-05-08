namespace GilsApi.Models;

public partial class Recommendation
{
    public string IdRecommendation { get; set; } = null!;

    public string? UserId { get; set; }

    public string? TrackId { get; set; }

    public string? ReasonId { get; set; }

    public virtual Reason? Reason { get; set; }

    public virtual Track? Track { get; set; }

    public virtual User? User { get; set; }
}
