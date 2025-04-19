using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MusAPI.Models;

public partial class MusicAppContext : DbContext
{
    public MusicAppContext(DbContextOptions<MusicAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<ListeningHistory> ListeningHistories { get; set; }

    public virtual DbSet<OfflineTrack> OfflineTracks { get; set; }

    public virtual DbSet<Playlist> Playlists { get; set; }

    public virtual DbSet<PlaylistTrack> PlaylistTracks { get; set; }

    public virtual DbSet<Track> Tracks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPreference> UserPreferences { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=147.45.196.64;Port=5432;Database=music_app;Username=strhzy;Password=12345678");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(e => e.ArtistId).HasName("artists_pkey");

            entity.ToTable("artists");

            entity.Property(e => e.ArtistId).HasColumnName("artist_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("genres_pkey");

            entity.ToTable("genres");

            entity.HasIndex(e => e.Name, "genres_name_key").IsUnique();

            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ListeningHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("listening_history_pkey");

            entity.ToTable("listening_history");

            entity.HasIndex(e => e.UserId, "idx_listening_history_user_id");

            entity.Property(e => e.HistoryId).HasColumnName("history_id");
            entity.Property(e => e.ListenedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("listened_at");
            entity.Property(e => e.TrackId).HasColumnName("track_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Track).WithMany(p => p.ListeningHistories)
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("listening_history_track_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.ListeningHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("listening_history_user_id_fkey");
        });

        modelBuilder.Entity<OfflineTrack>(entity =>
        {
            entity.HasKey(e => e.OfflineId).HasName("offline_tracks_pkey");

            entity.ToTable("offline_tracks");

            entity.Property(e => e.OfflineId).HasColumnName("offline_id");
            entity.Property(e => e.DownloadedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("downloaded_at");
            entity.Property(e => e.OfflinePath)
                .HasMaxLength(255)
                .HasColumnName("offline_path");
            entity.Property(e => e.TrackId).HasColumnName("track_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Track).WithMany(p => p.OfflineTracks)
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("offline_tracks_track_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.OfflineTracks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("offline_tracks_user_id_fkey");
        });

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.HasKey(e => e.PlaylistId).HasName("playlists_pkey");

            entity.ToTable("playlists");

            entity.Property(e => e.PlaylistId).HasColumnName("playlist_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Playlists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("playlists_user_id_fkey");
        });

        modelBuilder.Entity<PlaylistTrack>(entity =>
        {
            entity.HasKey(e => new { e.PlaylistId, e.TrackId }).HasName("playlist_tracks_pkey");

            entity.ToTable("playlist_tracks");

            entity.Property(e => e.PlaylistId).HasColumnName("playlist_id");
            entity.Property(e => e.TrackId).HasColumnName("track_id");
            entity.Property(e => e.AddedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("added_at");

            entity.HasOne(d => d.Playlist).WithMany(p => p.PlaylistTracks)
                .HasForeignKey(d => d.PlaylistId)
                .HasConstraintName("playlist_tracks_playlist_id_fkey");

            entity.HasOne(d => d.Track).WithMany(p => p.PlaylistTracks)
                .HasForeignKey(d => d.TrackId)
                .HasConstraintName("playlist_tracks_track_id_fkey");
        });

        modelBuilder.Entity<Track>(entity =>
        {
            entity.HasKey(e => e.TrackId).HasName("tracks_pkey");

            entity.ToTable("tracks");

            entity.HasIndex(e => e.ArtistId, "idx_tracks_artist_id");

            entity.HasIndex(e => e.GenreId, "idx_tracks_genre_id");

            entity.HasIndex(e => e.Title, "idx_tracks_title");

            entity.Property(e => e.TrackId).HasColumnName("track_id");
            entity.Property(e => e.ArtistId).HasColumnName("artist_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.FileUrl)
                .HasMaxLength(255)
                .HasColumnName("file_url");
            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.OfflinePath)
                .HasMaxLength(255)
                .HasColumnName("offline_path");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");

            entity.HasOne(d => d.Artist).WithMany(p => p.Tracks)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("tracks_artist_id_fkey");

            entity.HasOne(d => d.Genre).WithMany(p => p.Tracks)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("tracks_genre_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.ResetToken)
                .HasMaxLength(255)
                .HasColumnName("reset_token");
            entity.Property(e => e.ResetTokenExpiry)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("reset_token_expiry");
            entity.Property(e => e.ThemePreference)
                .HasMaxLength(10)
                .HasDefaultValueSql("'light'::character varying")
                .HasColumnName("theme_preference");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserPreference>(entity =>
        {
            entity.HasKey(e => e.PreferenceId).HasName("user_preferences_pkey");

            entity.ToTable("user_preferences");

            entity.HasIndex(e => e.UserId, "idx_user_preferences_user_id");

            entity.Property(e => e.PreferenceId).HasColumnName("preference_id");
            entity.Property(e => e.ArtistId).HasColumnName("artist_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Weight)
                .HasDefaultValue(1)
                .HasColumnName("weight");

            entity.HasOne(d => d.Artist).WithMany(p => p.UserPreferences)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("user_preferences_artist_id_fkey");

            entity.HasOne(d => d.Genre).WithMany(p => p.UserPreferences)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("user_preferences_genre_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserPreferences)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("user_preferences_user_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
