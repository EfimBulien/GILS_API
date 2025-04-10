namespace GilsApi.Models;

public partial class Attachment
{
    public Guid IdAttachment { get; set; }

    public Guid PostId { get; set; }

    public string Attachment1 { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;
}
