namespace GilsApi.Models;

public partial class SubscriptionType
{
    public string IdSubscriptionType { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string DurationId { get; set; } = null!;

    public virtual Duration Duration { get; set; } = null!;

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}
