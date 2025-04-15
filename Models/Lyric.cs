using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class Lyric
{
    public string IdLyric { get; set; } = null!;

    public string TrackId { get; set; } = null!;

    public string TextRu { get; set; } = null!;

    public string TextEn { get; set; } = null!;

    public string? TextAuthor { get; set; }

    public virtual Track Track { get; set; } = null!;
}
