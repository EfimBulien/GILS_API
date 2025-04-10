namespace GilsApi.Models;

public partial class Sample
{
    public Guid IdSample { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Cover { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<ShortVideo> ShortVideos { get; set; } = new List<ShortVideo>();

    public virtual User User { get; set; } = null!;
}
