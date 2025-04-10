namespace GilsApi.Models;

public partial class Reaction
{
    public Guid IdReaction { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ReactionsUsersPost> ReactionsUsersPosts { get; set; } = new List<ReactionsUsersPost>();
}
