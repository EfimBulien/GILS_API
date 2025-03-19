using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class User
{
    public int IdUser { get; set; }

    public decimal Phone { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly Birthday { get; set; }

    public int RoleId { get; set; }

    public int CityId { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual ICollection<TracksUser> TracksUsers { get; set; } = new List<TracksUser>();
}
