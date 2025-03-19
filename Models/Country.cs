using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class Country
{
    public int IdCountry { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
