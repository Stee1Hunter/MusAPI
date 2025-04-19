using System;
using System.Collections.Generic;

namespace MusAPI.Models;

public partial class Track
{
    public int TrackId { get; set; }

    public string Title { get; set; } = null!;

    public int? ArtistId { get; set; }

    public int? GenreId { get; set; }

    public int Duration { get; set; }

    public string FileUrl { get; set; } = null!;

    public string? OfflinePath { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Artist? Artist { get; set; }

    public virtual Genre? Genre { get; set; }

    public virtual ICollection<ListeningHistory> ListeningHistories { get; set; } = new List<ListeningHistory>();

    public virtual ICollection<OfflineTrack> OfflineTracks { get; set; } = new List<OfflineTrack>();

    public virtual ICollection<PlaylistTrack> PlaylistTracks { get; set; } = new List<PlaylistTrack>();
}
