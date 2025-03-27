using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class Reaction
{
    public int IdReaction { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ReactionsUsersPost> ReactionsUsersPosts { get; set; } = new List<ReactionsUsersPost>();
}
