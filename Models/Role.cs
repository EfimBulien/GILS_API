namespace GilsApi.Models;

public partial class Role
{
    public Guid IdRole { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
