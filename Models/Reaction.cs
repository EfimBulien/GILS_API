namespace GilsApi.Models;

public partial class Reaction
{
    public string IdReaction { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<ReactionsUsersPost> ReactionsUsersPosts { get; set; } = new List<ReactionsUsersPost>();
}
