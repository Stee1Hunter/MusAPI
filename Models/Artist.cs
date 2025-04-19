using System;
using System.Collections.Generic;

namespace MusAPI.Models;

public partial class Artist
{
    public int ArtistId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();

    public virtual ICollection<UserPreference> UserPreferences { get; set; } = new List<UserPreference>();
}
