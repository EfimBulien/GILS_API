namespace GilsApi.Models;

public partial class Mark
{
    public Guid IdMark { get; set; }

    public Guid LyricId { get; set; }

    public string TextRow { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual Lyric Lyric { get; set; } = null!;
}
