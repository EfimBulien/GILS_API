namespace GilsApi.Models;

public partial class Comment
{
    public string IdComment { get; set; } = null!;

    public string PostId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
