namespace GilsApi.Models;

public partial class Action
{
    public Guid IdAction { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();
}
