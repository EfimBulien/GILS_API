using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class SubscriptionType
{
    public int IdSubscriptionType { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}
