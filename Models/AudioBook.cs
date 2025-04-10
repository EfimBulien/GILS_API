namespace GilsApi.Models;

public partial class AudioBook
{
    public Guid IdAudioBook { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Cover { get; set; } = null!;

    public string? Description { get; set; }

    public string? Text { get; set; }

    public string? AudioBook1 { get; set; }

    public virtual User User { get; set; } = null!;
}
