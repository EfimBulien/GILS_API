namespace GilsApi.Models;

public partial class Like
{
    public Guid IdLike { get; set; }

    public Guid UserId { get; set; }

    public Guid AlbumId { get; set; }

    public virtual Album Album { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
