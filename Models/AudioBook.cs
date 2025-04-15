using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class AudioBook
{
    public string IdAudioBook { get; set; } = null!;

    public string ArtistId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Cover { get; set; } = null!;

    public string? Description { get; set; }

    public string? Text { get; set; }

    public string? AudioBook1 { get; set; }

    public virtual Artist Artist { get; set; } = null!;
}
