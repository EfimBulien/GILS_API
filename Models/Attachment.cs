using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class Attachment
{
    public int IdAttachment { get; set; }

    public int PostId { get; set; }

    public string Attachment1 { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;
}
