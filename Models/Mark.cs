using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class Mark
{
    public int IdMark { get; set; }

    public int LyricId { get; set; }

    public string TextRow { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual Lyric Lyric { get; set; } = null!;
}
