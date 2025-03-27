using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class Share
{
    public int IdShare { get; set; }

    public int UserIdSharedBy { get; set; }

    public int UserIdSharedWith { get; set; }

    public int AlbumId { get; set; }

    public virtual Album Album { get; set; } = null!;

    public virtual User UserIdSharedByNavigation { get; set; } = null!;

    public virtual User UserIdSharedWithNavigation { get; set; } = null!;
}
