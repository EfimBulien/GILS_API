namespace GilsApi.Models;

public partial class Subscription
{
    public Guid IdSubscription { get; set; }

    public Guid UserId { get; set; }

    public Guid SubscriptionTypeId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public Guid? ArtistId { get; set; }

    public virtual User? Artist { get; set; }

    public virtual SubscriptionType SubscriptionType { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
