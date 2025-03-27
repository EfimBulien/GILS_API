using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class Comment
{
    public int IdComment { get; set; }

    public int PostId { get; set; }

    public int UserId { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
