using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class UsersTrack
{
    public int IdUserTrack { get; set; }

    public int UserId { get; set; }

    public int TrackId { get; set; }

    public virtual Track Track { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
