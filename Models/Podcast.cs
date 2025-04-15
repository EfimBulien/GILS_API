using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class Podcast
{
    public string IdPoscast { get; set; } = null!;

    public string ArtistId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Text { get; set; } = null!;

    public string? Video { get; set; }

    public string Cover { get; set; } = null!;

    public string? VideoCover { get; set; }

    public virtual Artist Artist { get; set; } = null!;
}
