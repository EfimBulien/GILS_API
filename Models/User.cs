namespace GilsApi.Models;

public partial class User
{
    public string IdUser { get; set; } = null!;

    public decimal Phone { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly Birthday { get; set; }

    public string? Photo { get; set; }

    public string RoleId { get; set; } = null!;

    public string CityId { get; set; } = null!;

    public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();

    public virtual City City { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual ICollection<ReactionsUsersPost> ReactionsUsersPosts { get; set; } = new List<ReactionsUsersPost>();

    public virtual ICollection<Recommendation> Recommendations { get; set; } = new List<Recommendation>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Share> ShareUserIdSharedByNavigations { get; set; } = new List<Share>();

    public virtual ICollection<Share> ShareUserIdSharedWithNavigations { get; set; } = new List<Share>();

    public virtual ICollection<SimilarArtist> SimilarArtistArtistId1Navigations { get; set; } = new List<SimilarArtist>();

    public virtual ICollection<SimilarArtist> SimilarArtistArtistId2Navigations { get; set; } = new List<SimilarArtist>();

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual ICollection<UsersTrack> UsersTracks { get; set; } = new List<UsersTrack>();
}
