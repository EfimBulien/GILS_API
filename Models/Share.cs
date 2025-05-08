namespace GilsApi.Models;

public partial class Share
{
    public string IdShare { get; set; } = null!;

    public string UserIdSharedBy { get; set; } = null!;

    public string UserIdSharedWith { get; set; } = null!;

    public string AlbumId { get; set; } = null!;

    public virtual Album Album { get; set; } = null!;

    public virtual User UserIdSharedByNavigation { get; set; } = null!;

    public virtual User UserIdSharedWithNavigation { get; set; } = null!;
}
