namespace GilsApi.Models;

public partial class Comment
{
    public Guid IdComment { get; set; }

    public Guid PostId { get; set; }

    public Guid UserId { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
