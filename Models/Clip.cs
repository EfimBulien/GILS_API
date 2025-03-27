using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class Clip
{
    public int IdClip { get; set; }

    public int TrackId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Clip1 { get; set; } = null!;

    public virtual Track Track { get; set; } = null!;
}
