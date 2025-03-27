using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class Activity
{
    public int IdActivity { get; set; }

    public int UserId { get; set; }

    public int TrackId { get; set; }

    public int ActionId { get; set; }

    public DateTime? Timestamp { get; set; }

    public virtual Action Action { get; set; } = null!;

    public virtual Track Track { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
