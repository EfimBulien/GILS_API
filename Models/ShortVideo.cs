namespace GilsApi.Models;

public partial class ShortVideo
{
    public string IdShortVideo { get; set; } = null!;

    public string SampleId { get; set; } = null!;

    public virtual Sample Sample { get; set; } = null!;
}
