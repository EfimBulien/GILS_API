namespace GilsApi.Models;

public partial class ReactionsUsersPost
{
    public Guid IdReactionUserPost { get; set; }

    public Guid ReactionId { get; set; }

    public Guid UserId { get; set; }

    public Guid PostId { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual Reaction Reaction { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
