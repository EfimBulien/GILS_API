using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class Subscription
{
    public int IdSubscription { get; set; }

    public int UserId { get; set; }

    public int SubscriptionTypeId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int? ArtistId { get; set; }

    public virtual User? Artist { get; set; }

    public virtual SubscriptionType SubscriptionType { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
