namespace GilsApi.Models;

public partial class Artist
{
    public string IdArtist { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Photo { get; set; }

    public string? SocialLinks { get; set; }

    public string? MerchShop { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();

    public virtual ICollection<AudioBook> AudioBooks { get; set; } = new List<AudioBook>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Podcast> Podcasts { get; set; } = new List<Podcast>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<Sample> Samples { get; set; } = new List<Sample>();

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}
