namespace GilsApi.Models;

public partial class Lyric
{
    public Guid IdLyric { get; set; }

    public Guid TrackId { get; set; }

    public string TextRu { get; set; } = null!;

    public string TextEn { get; set; } = null!;

    public string? TextAuthor { get; set; }

    public virtual Track Track { get; set; } = null!;
}
