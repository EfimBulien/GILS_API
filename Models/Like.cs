using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class Like
{
    public int IdLike { get; set; }

    public int UserId { get; set; }

    public int AlbumId { get; set; }

    public virtual Album Album { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
