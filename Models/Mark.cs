namespace GilsApi.Models;

public partial class Mark
{
    public string IdMark { get; set; } = null!;

    public string LyricId { get; set; } = null!;

    public string TextRow { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual Lyric Lyric { get; set; } = null!;
}
