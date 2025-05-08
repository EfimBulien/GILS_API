namespace GilsApi.Models;

public partial class Reason
{
    public string IdReason { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Recommendation> Recommendations { get; set; } = new List<Recommendation>();
}
