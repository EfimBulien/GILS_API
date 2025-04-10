namespace GilsApi.Models;

public partial class SubscriptionType
{
    public Guid IdSubscriptionType { get; set; }

    public string Name { get; set; } = null!;

    public Guid DurationId { get; set; }

    public virtual Duration Duration { get; set; } = null!;

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}
