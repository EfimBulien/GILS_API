namespace GilsApi.Models;

public partial class Duration
{
    public string IdDuration { get; set; } = null!;

    public string Text { get; set; } = null!;

    public int Days { get; set; }

    public virtual ICollection<SubscriptionType> SubscriptionTypes { get; set; } = new List<SubscriptionType>();
}
