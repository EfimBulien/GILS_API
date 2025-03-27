using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class Reason
{
    public int IdReason { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Recommendation> Recommendations { get; set; } = new List<Recommendation>();
}
