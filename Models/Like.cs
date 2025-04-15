using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class Like
{
    public string IdLike { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string AlbumId { get; set; } = null!;

    public virtual Album Album { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
