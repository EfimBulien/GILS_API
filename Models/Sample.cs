using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class Sample
{
    public string IdSample { get; set; } = null!;

    public string ArtistId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Cover { get; set; } = null!;

    public string? Description { get; set; }

    public virtual Artist Artist { get; set; } = null!;

    public virtual ICollection<ShortVideo> ShortVideos { get; set; } = new List<ShortVideo>();
}
