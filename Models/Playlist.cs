using System;
using System.Collections.Generic;

namespace MusAPI.Models;

public partial class Playlist
{
    public int PlaylistId { get; set; }

    public int? UserId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<PlaylistTrack> PlaylistTracks { get; set; } = new List<PlaylistTrack>();

    public virtual User? User { get; set; }
}
