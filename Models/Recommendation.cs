namespace GilsApi.Models;

public partial class Recommendation
{
    public Guid IdRecommendation { get; set; }

    public Guid? UserId { get; set; }

    public Guid? TrackId { get; set; }

    public Guid? ReasonId { get; set; }

    public virtual Reason? Reason { get; set; }

    public virtual Track? Track { get; set; }

    public virtual User? User { get; set; }
}
