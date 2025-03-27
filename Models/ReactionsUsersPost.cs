using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class ReactionsUsersPost
{
    public int IdReactionUserPost { get; set; }

    public int ReactionId { get; set; }

    public int UserId { get; set; }

    public int PostId { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual Reaction Reaction { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
