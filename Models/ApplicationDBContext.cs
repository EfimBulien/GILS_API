using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GilsApi.Models;

public partial class ApplicationDBContext : DbContext
{
    public ApplicationDBContext()
    {
    }

    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<SubscriptionType> SubscriptionTypes { get; set; }

    public virtual DbSet<Track> Tracks { get; set; }

    public virtual DbSet<TracksUser> TracksUsers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=gils_db;Username=postgres;Password=1008");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.IdCity).HasName("cities_pkey");

            entity.ToTable("cities");

            entity.Property(e => e.IdCity).HasColumnName("id_city");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cities_country_id_fkey");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.IdCountry).HasName("countries_pkey");

            entity.ToTable("countries");

            entity.Property(e => e.IdCountry).HasColumnName("id_country");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.IdSubscription).HasName("subscriptions_pkey");

            entity.ToTable("subscriptions");

            entity.Property(e => e.IdSubscription).HasColumnName("id_subscription");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.SubscriptionTypeId).HasColumnName("subscription_type_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.SubscriptionType).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.SubscriptionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("subscriptions_subscription_type_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("subscriptions_user_id_fkey");
        });

        modelBuilder.Entity<SubscriptionType>(entity =>
        {
            entity.HasKey(e => e.IdSubscriptionType).HasName("subscription_types_pkey");

            entity.ToTable("subscription_types");

            entity.Property(e => e.IdSubscriptionType).HasColumnName("id_subscription_type");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Track>(entity =>
        {
            entity.HasKey(e => e.IdTrack).HasName("tracks_pkey");

            entity.ToTable("tracks");

            entity.Property(e => e.IdTrack).HasColumnName("id_track");
            entity.Property(e => e.CoverUrl).HasColumnName("cover_url");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.TrackUrl).HasColumnName("track_url");
        });

        modelBuilder.Entity<TracksUser>(entity =>
        {
            entity.HasKey(e => e.IdTrackUser).HasName("tracks_users_pkey");

            entity.ToTable("tracks_users");

            entity.Property(e => e.IdTrackUser).HasColumnName("id_track_user");
            entity.Property(e => e.TrackId).HasColumnName("track_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Track).WithMany(p => p.TracksUsers)
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tracks_users_track_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.TracksUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tracks_users_user_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasPrecision(15)
                .HasColumnName("phone");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.City).WithMany(p => p.Users)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_city_id_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_role_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
