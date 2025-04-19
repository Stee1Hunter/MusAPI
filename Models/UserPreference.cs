using System;
using System.Collections.Generic;

namespace MusAPI.Models;

public partial class UserPreference
{
    public int PreferenceId { get; set; }

    public int? UserId { get; set; }

    public int? GenreId { get; set; }

    public int? ArtistId { get; set; }

    public int? Weight { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Artist? Artist { get; set; }

    public virtual Genre? Genre { get; set; }

    public virtual User? User { get; set; }
}
