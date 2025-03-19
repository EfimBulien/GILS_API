using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class Track
{
    public int IdTrack { get; set; }

    public string? Description { get; set; }

    public string TrackUrl { get; set; } = null!;

    public string? CoverUrl { get; set; }

    public virtual ICollection<TracksUser> TracksUsers { get; set; } = new List<TracksUser>();
}
