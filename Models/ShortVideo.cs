namespace GilsApi.Models;

public partial class ShortVideo
{
    public Guid IdShortVideo { get; set; }

    public Guid SampleId { get; set; }

    public virtual Sample Sample { get; set; } = null!;
}
