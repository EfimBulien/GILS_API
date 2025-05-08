namespace GilsApi.Models;

public partial class Subscription
{
    public string IdSubscription { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string SubscriptionTypeId { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string ArtistId { get; set; } = null!;

    public virtual Artist Artist { get; set; } = null!;

    public virtual SubscriptionType SubscriptionType { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
