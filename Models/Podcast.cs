namespace GilsApi.Models;

public partial class Podcast
{
    public Guid IdPoscast { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Text { get; set; } = null!;

    public string? Video { get; set; }

    public string Cover { get; set; } = null!;

    public string? VideoCover { get; set; }

    public virtual User User { get; set; } = null!;
}
