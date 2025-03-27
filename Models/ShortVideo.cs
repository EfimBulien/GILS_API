using System;
using System.Collections.Generic;

namespace GilsApi.Models;

public partial class ShortVideo
{
    public int IdShortVideo { get; set; }

    public int SampleId { get; set; }

    public virtual Sample Sample { get; set; } = null!;
}
