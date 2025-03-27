using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class AudioBook
{
    public int IdAudioBook { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Cover { get; set; } = null!;

    public string? Description { get; set; }

    public string? Text { get; set; }

    public string? AudioBook1 { get; set; }

    public virtual User User { get; set; } = null!;
}
