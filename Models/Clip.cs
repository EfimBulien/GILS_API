using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class Clip
{
    public string IdClip { get; set; } = null!;

    public string TrackId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Clip1 { get; set; } = null!;

    public virtual Track Track { get; set; } = null!;
}
