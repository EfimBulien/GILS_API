using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class ReactionsUsersPost
{
    public string IdReactionUserPost { get; set; } = null!;

    public string ReactionId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string PostId { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;

    public virtual Reaction Reaction { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
