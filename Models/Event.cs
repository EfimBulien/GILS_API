using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class Event
{
    public int IdEvent { get; set; }

    public int UserId { get; set; }

    public DateOnly Date { get; set; }

    public string Place { get; set; } = null!;

    public int CountryId { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
