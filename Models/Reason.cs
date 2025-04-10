namespace GilsApi.Models;

public partial class Reason
{
    public Guid IdReason { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Recommendation> Recommendations { get; set; } = new List<Recommendation>();
}
