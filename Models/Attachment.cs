namespace GilsApi.Models;

public partial class Attachment
{
    public string IdAttachment { get; set; } = null!;

    public string PostId { get; set; } = null!;

    public string Attachment1 { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;
}
