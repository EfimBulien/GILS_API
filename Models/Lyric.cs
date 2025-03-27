using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class Lyric
{
    public int IdLyric { get; set; }

    public int TrackId { get; set; }

    public string TextRu { get; set; } = null!;

    public string TextEn { get; set; } = null!;

    public string? TextAuthor { get; set; }

    public virtual Track Track { get; set; } = null!;
}
