namespace GilsApi.Models;

public partial class Share
{
    public Guid IdShare { get; set; }

    public Guid UserIdSharedBy { get; set; }

    public Guid UserIdSharedWith { get; set; }

    public Guid AlbumId { get; set; }

    public virtual Album Album { get; set; } = null!;

    public virtual User UserIdSharedByNavigation { get; set; } = null!;

    public virtual User UserIdSharedWithNavigation { get; set; } = null!;
}
