namespace GilsApi.Models;

public partial class Duration
{
    public Guid IdDuration { get; set; }

    public string Text { get; set; } = null!;

    public int Days { get; set; }

    public virtual ICollection<SubscriptionType> SubscriptionTypes { get; set; } = new List<SubscriptionType>();
}
