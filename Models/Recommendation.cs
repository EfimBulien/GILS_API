using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class Recommendation
{
    public int IdRecommendation { get; set; }

    public int? UserId { get; set; }

    public int? TrackId { get; set; }

    public int? ReasonId { get; set; }

    public virtual Reason? Reason { get; set; }

    public virtual Track? Track { get; set; }

    public virtual User? User { get; set; }
}
