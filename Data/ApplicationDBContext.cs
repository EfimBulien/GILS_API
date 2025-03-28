using GilsApi.Models;
using Microsoft.EntityFrameworkCore;
using Action = GilsApi.Models.Action;

namespace GilsApi.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext() { }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public virtual DbSet<Action> Actions { get; set; }

    public virtual DbSet<Activity> Activities { get; set; }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Attachment> Attachments { get; set; }

    public virtual DbSet<AudioBook> AudioBooks { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Clip> Clips { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Duration> Durations { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<GenresTrack> GenresTracks { get; set; }

    public virtual DbSet<Like> Likes { get; set; }

    public virtual DbSet<Lyric> Lyrics { get; set; }

    public virtual DbSet<Mark> Marks { get; set; }

    public virtual DbSet<Podcast> Podcasts { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Reaction> Reactions { get; set; }

    public virtual DbSet<ReactionsUsersPost> ReactionsUsersPosts { get; set; }

    public virtual DbSet<Reason> Reasons { get; set; }

    public virtual DbSet<Recommendation> Recommendations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sample> Samples { get; set; }

    public virtual DbSet<Share> Shares { get; set; }

    public virtual DbSet<ShortVideo> ShortVideos { get; set; }

    public virtual DbSet<SimilarArtist> SimilarArtists { get; set; }

    public virtual DbSet<SimilarTrack> SimilarTracks { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<SubscriptionType> SubscriptionTypes { get; set; }

    public virtual DbSet<Track> Tracks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsersTrack> UsersTracks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder) => 
        builder.UseNpgsql("Host=localhost;Port=5432;Database=gils;Username=postgres;Password=1008");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Action>(entity =>
        {
            entity.HasKey(e => e.IdAction).HasName("actions_pkey");

            entity.ToTable("actions");

            entity.Property(e => e.IdAction).HasColumnName("id_action");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Activity>(entity =>
        {
            entity.HasKey(e => e.IdActivity).HasName("activities_pkey");

            entity.ToTable("activities");

            entity.Property(e => e.IdActivity).HasColumnName("id_activity");
            entity.Property(e => e.ActionId).HasColumnName("action_id");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("timestamp");
            entity.Property(e => e.TrackId).HasColumnName("track_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Action).WithMany(p => p.Activities)
                .HasForeignKey(d => d.ActionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("activities_action_id_fkey");

            entity.HasOne(d => d.Track).WithMany(p => p.Activities)
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("activities_track_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Activities)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("activities_user_id_fkey");
        });

        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.IdAlbum).HasName("albums_pkey");

            entity.ToTable("albums");

            entity.Property(e => e.IdAlbum).HasColumnName("id_album");
            entity.Property(e => e.Cover).HasColumnName("cover");
            entity.Property(e => e.IsPopular)
                .HasDefaultValue(false)
                .HasColumnName("is_popular");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.ReleaseDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("release_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.VideoCover).HasColumnName("video_cover");

            entity.HasOne(d => d.User).WithMany(p => p.Albums)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("albums_user_id_fkey");
        });

        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.HasKey(e => e.IdAttachment).HasName("attachments_pkey");

            entity.ToTable("attachments");

            entity.Property(e => e.IdAttachment).HasColumnName("id_attachment");
            entity.Property(e => e.Attachment1).HasColumnName("attachment");
            entity.Property(e => e.PostId).HasColumnName("post_id");

            entity.HasOne(d => d.Post).WithMany(p => p.Attachments)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("attachments_post_id_fkey");
        });

        modelBuilder.Entity<AudioBook>(entity =>
        {
            entity.HasKey(e => e.IdAudioBook).HasName("audio_books_pkey");

            entity.ToTable("audio_books");

            entity.Property(e => e.IdAudioBook).HasColumnName("id_audio_book");
            entity.Property(e => e.AudioBook1).HasColumnName("audio_book");
            entity.Property(e => e.Cover).HasColumnName("cover");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Text).HasColumnName("text");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.AudioBooks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("audio_books_user_id_fkey");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.IdCity).HasName("cities_pkey");

            entity.ToTable("cities");

            entity.Property(e => e.IdCity).HasColumnName("id_city");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cities_country_id_fkey");
        });

        modelBuilder.Entity<Clip>(entity =>
        {
            entity.HasKey(e => e.IdClip).HasName("clips_pkey");

            entity.ToTable("clips");

            entity.Property(e => e.IdClip).HasColumnName("id_clip");
            entity.Property(e => e.Clip1).HasColumnName("clip");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.TrackId).HasColumnName("track_id");

            entity.HasOne(d => d.Track).WithMany(p => p.Clips)
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("clips_track_id_fkey");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.IdComment).HasName("comments_pkey");

            entity.ToTable("comments");

            entity.Property(e => e.IdComment).HasColumnName("id_comment");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comments_post_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comments_user_id_fkey");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.IdCountry).HasName("countries_pkey");

            entity.ToTable("countries");

            entity.Property(e => e.IdCountry).HasColumnName("id_country");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Duration>(entity =>
        {
            entity.HasKey(e => e.IdDuration).HasName("durations_pkey");

            entity.ToTable("durations");

            entity.Property(e => e.IdDuration).HasColumnName("id_duration");
            entity.Property(e => e.Days).HasColumnName("days");
            entity.Property(e => e.Text)
                .HasMaxLength(255)
                .HasColumnName("text");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.IdEvent).HasName("events_pkey");

            entity.ToTable("events");

            entity.Property(e => e.IdEvent).HasColumnName("id_event");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Place).HasColumnName("place");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Country).WithMany(p => p.Events)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("events_country_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Events)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("events_user_id_fkey");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.IdGenre).HasName("genres_pkey");

            entity.ToTable("genres");

            entity.Property(e => e.IdGenre).HasColumnName("id_genre");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<GenresTrack>(entity =>
        {
            entity.HasKey(e => e.IdGenreTrack).HasName("genres_tracks_pkey");

            entity.ToTable("genres_tracks");

            entity.Property(e => e.IdGenreTrack).HasColumnName("id_genre_track");
            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.TrackId).HasColumnName("track_id");

            entity.HasOne(d => d.Genre).WithMany(p => p.GenresTracks)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("genres_tracks_genre_id_fkey");

            entity.HasOne(d => d.Track).WithMany(p => p.GenresTracks)
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("genres_tracks_track_id_fkey");
        });

        modelBuilder.Entity<Like>(entity =>
        {
            entity.HasKey(e => e.IdLike).HasName("likes_pkey");

            entity.ToTable("likes");

            entity.Property(e => e.IdLike).HasColumnName("id_like");
            entity.Property(e => e.AlbumId).HasColumnName("album_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Album).WithMany(p => p.Likes)
                .HasForeignKey(d => d.AlbumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("likes_album_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Likes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("likes_user_id_fkey");
        });

        modelBuilder.Entity<Lyric>(entity =>
        {
            entity.HasKey(e => e.IdLyric).HasName("lyrics_pkey");

            entity.ToTable("lyrics");

            entity.Property(e => e.IdLyric).HasColumnName("id_lyric");
            entity.Property(e => e.TextAuthor)
                .HasMaxLength(255)
                .HasColumnName("text_author");
            entity.Property(e => e.TextEn).HasColumnName("text_en");
            entity.Property(e => e.TextRu).HasColumnName("text_ru");
            entity.Property(e => e.TrackId).HasColumnName("track_id");

            entity.HasOne(d => d.Track).WithMany(p => p.Lyrics)
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lyrics_track_id_fkey");
        });

        modelBuilder.Entity<Mark>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("marks");

            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IdMark)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_mark");
            entity.Property(e => e.LyricId).HasColumnName("lyric_id");
            entity.Property(e => e.TextRow)
                .HasMaxLength(255)
                .HasColumnName("text_row");

            entity.HasOne(d => d.Lyric).WithMany()
                .HasForeignKey(d => d.LyricId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("marks_lyric_id_fkey");
        });

        modelBuilder.Entity<Podcast>(entity =>
        {
            entity.HasKey(e => e.IdPoscast).HasName("podcasts_pkey");

            entity.ToTable("podcasts");

            entity.Property(e => e.IdPoscast).HasColumnName("id_poscast");
            entity.Property(e => e.Cover).HasColumnName("cover");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Text).HasColumnName("text");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Video).HasColumnName("video");
            entity.Property(e => e.VideoCover).HasColumnName("video_cover");

            entity.HasOne(d => d.User).WithMany(p => p.Podcasts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("podcasts_user_id_fkey");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.IdPost).HasName("posts_pkey");

            entity.ToTable("posts");

            entity.Property(e => e.IdPost).HasColumnName("id_post");
            entity.Property(e => e.PublishTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("publish_time");
            entity.Property(e => e.Text).HasColumnName("text");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Posts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("posts_user_id_fkey");
        });

        modelBuilder.Entity<Reaction>(entity =>
        {
            entity.HasKey(e => e.IdReaction).HasName("reactions_pkey");

            entity.ToTable("reactions");

            entity.Property(e => e.IdReaction).HasColumnName("id_reaction");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ReactionsUsersPost>(entity =>
        {
            entity.HasKey(e => e.IdReactionUserPost).HasName("reactions_users_posts_pkey");

            entity.ToTable("reactions_users_posts");

            entity.Property(e => e.IdReactionUserPost).HasColumnName("id_reaction_user_post");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.ReactionId).HasColumnName("reaction_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Post).WithMany(p => p.ReactionsUsersPosts)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reactions_users_posts_post_id_fkey");

            entity.HasOne(d => d.Reaction).WithMany(p => p.ReactionsUsersPosts)
                .HasForeignKey(d => d.ReactionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reactions_users_posts_reaction_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.ReactionsUsersPosts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reactions_users_posts_user_id_fkey");
        });

        modelBuilder.Entity<Reason>(entity =>
        {
            entity.HasKey(e => e.IdReason).HasName("reasons_pkey");

            entity.ToTable("reasons");

            entity.Property(e => e.IdReason).HasColumnName("id_reason");
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Recommendation>(entity =>
        {
            entity.HasKey(e => e.IdRecommendation).HasName("recommendations_pkey");

            entity.ToTable("recommendations");

            entity.Property(e => e.IdRecommendation).HasColumnName("id_recommendation");
            entity.Property(e => e.ReasonId).HasColumnName("reason_id");
            entity.Property(e => e.TrackId).HasColumnName("track_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Reason).WithMany(p => p.Recommendations)
                .HasForeignKey(d => d.ReasonId)
                .HasConstraintName("recommendations_reason_id_fkey");

            entity.HasOne(d => d.Track).WithMany(p => p.Recommendations)
                .HasForeignKey(d => d.TrackId)
                .HasConstraintName("recommendations_track_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Recommendations)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("recommendations_user_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Sample>(entity =>
        {
            entity.HasKey(e => e.IdSample).HasName("samples_pkey");

            entity.ToTable("samples");

            entity.Property(e => e.IdSample).HasColumnName("id_sample");
            entity.Property(e => e.Cover).HasColumnName("cover");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Samples)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("samples_user_id_fkey");
        });

        modelBuilder.Entity<Share>(entity =>
        {
            entity.HasKey(e => e.IdShare).HasName("shares_pkey");

            entity.ToTable("shares");

            entity.Property(e => e.IdShare).HasColumnName("id_share");
            entity.Property(e => e.AlbumId).HasColumnName("album_id");
            entity.Property(e => e.UserIdSharedBy).HasColumnName("user_id_shared_by");
            entity.Property(e => e.UserIdSharedWith).HasColumnName("user_id_shared_with");

            entity.HasOne(d => d.Album).WithMany(p => p.Shares)
                .HasForeignKey(d => d.AlbumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("shares_album_id_fkey");

            entity.HasOne(d => d.UserIdSharedByNavigation).WithMany(p => p.ShareUserIdSharedByNavigations)
                .HasForeignKey(d => d.UserIdSharedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("shares_user_id_shared_by_fkey");

            entity.HasOne(d => d.UserIdSharedWithNavigation).WithMany(p => p.ShareUserIdSharedWithNavigations)
                .HasForeignKey(d => d.UserIdSharedWith)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("shares_user_id_shared_with_fkey");
        });

        modelBuilder.Entity<ShortVideo>(entity =>
        {
            entity.HasKey(e => e.IdShortVideo).HasName("short_videos_pkey");

            entity.ToTable("short_videos");

            entity.Property(e => e.IdShortVideo).HasColumnName("id_short_video");
            entity.Property(e => e.SampleId).HasColumnName("sample_id");

            entity.HasOne(d => d.Sample).WithMany(p => p.ShortVideos)
                .HasForeignKey(d => d.SampleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("short_videos_sample_id_fkey");
        });

        modelBuilder.Entity<SimilarArtist>(entity =>
        {
            entity.HasKey(e => e.IdSimilarArtist).HasName("similar_artists_pkey");

            entity.ToTable("similar_artists");

            entity.Property(e => e.IdSimilarArtist).HasColumnName("id_similar_artist");
            entity.Property(e => e.ArtistId1).HasColumnName("artist_id_1");
            entity.Property(e => e.ArtistId2).HasColumnName("artist_id_2");
            entity.Property(e => e.SimilarScore).HasColumnName("similar_score");

            entity.HasOne(d => d.ArtistId1Navigation).WithMany(p => p.SimilarArtistArtistId1Navigations)
                .HasForeignKey(d => d.ArtistId1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("similar_artists_artist_id_1_fkey");

            entity.HasOne(d => d.ArtistId2Navigation).WithMany(p => p.SimilarArtistArtistId2Navigations)
                .HasForeignKey(d => d.ArtistId2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("similar_artists_artist_id_2_fkey");
        });

        modelBuilder.Entity<SimilarTrack>(entity =>
        {
            entity.HasKey(e => e.IdSimilarTrack).HasName("similar_tracks_pkey");

            entity.ToTable("similar_tracks");

            entity.Property(e => e.IdSimilarTrack).HasColumnName("id_similar_track");
            entity.Property(e => e.SimilarScore).HasColumnName("similar_score");
            entity.Property(e => e.TrackId1).HasColumnName("track_id_1");
            entity.Property(e => e.TrackId2).HasColumnName("track_id_2");

            entity.HasOne(d => d.TrackId1Navigation).WithMany(p => p.SimilarTrackTrackId1Navigations)
                .HasForeignKey(d => d.TrackId1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("similar_tracks_track_id_1_fkey");

            entity.HasOne(d => d.TrackId2Navigation).WithMany(p => p.SimilarTrackTrackId2Navigations)
                .HasForeignKey(d => d.TrackId2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("similar_tracks_track_id_2_fkey");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.IdSubscription).HasName("subscriptions_pkey");

            entity.ToTable("subscriptions");

            entity.Property(e => e.IdSubscription).HasColumnName("id_subscription");
            entity.Property(e => e.ArtistId).HasColumnName("artist_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("start_date");
            entity.Property(e => e.SubscriptionTypeId).HasColumnName("subscription_type_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Artist).WithMany(p => p.SubscriptionArtists)
                .HasForeignKey(d => d.ArtistId)
                .HasConstraintName("subscriptions_artist_id_fkey");

            entity.HasOne(d => d.SubscriptionType).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.SubscriptionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("subscriptions_subscription_type_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.SubscriptionUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("subscriptions_user_id_fkey");
        });

        modelBuilder.Entity<SubscriptionType>(entity =>
        {
            entity.HasKey(e => e.IdSubscriptionType).HasName("subscription_types_pkey");

            entity.ToTable("subscription_types");

            entity.Property(e => e.IdSubscriptionType).HasColumnName("id_subscription_type");
            entity.Property(e => e.DurationId).HasColumnName("duration_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.HasOne(d => d.Duration).WithMany(p => p.SubscriptionTypes)
                .HasForeignKey(d => d.DurationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("subscription_types_duration_id_fkey");
        });

        modelBuilder.Entity<Track>(entity =>
        {
            entity.HasKey(e => e.IdTrack).HasName("tracks_pkey");

            entity.ToTable("tracks");

            entity.Property(e => e.IdTrack).HasColumnName("id_track");
            entity.Property(e => e.AlbumId).HasColumnName("album_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.IsPopular)
                .HasDefaultValue(false)
                .HasColumnName("is_popular");
            entity.Property(e => e.MainArtist)
                .HasMaxLength(255)
                .HasColumnName("main_artist");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Producer)
                .HasMaxLength(255)
                .HasColumnName("producer");
            entity.Property(e => e.ReleaseDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("release_date");
            entity.Property(e => e.Track1).HasColumnName("track");

            entity.HasOne(d => d.Album).WithMany(p => p.Tracks)
                .HasForeignKey(d => d.AlbumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tracks_album_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("last_name");
            entity.Property(e => e.MerchShop).HasColumnName("merch_shop");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasPrecision(15)
                .HasColumnName("phone");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.SocialLinks).HasColumnName("social_links");

            entity.HasOne(d => d.City).WithMany(p => p.Users)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_city_id_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_role_id_fkey");
        });

        modelBuilder.Entity<UsersTrack>(entity =>
        {
            entity.HasKey(e => e.IdUserTrack).HasName("users_tracks_pkey");

            entity.ToTable("users_tracks");

            entity.Property(e => e.IdUserTrack).HasColumnName("id_user_track");
            entity.Property(e => e.TrackId).HasColumnName("track_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Track).WithMany(p => p.UsersTracks)
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_tracks_track_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UsersTracks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_tracks_user_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
