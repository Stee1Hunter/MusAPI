using System;
using System.Collections.Generic;

namespace MusAPI.Models;

public partial class ListeningHistory
{
    public int HistoryId { get; set; }

    public int? UserId { get; set; }

    public int? TrackId { get; set; }

    public DateTime? ListenedAt { get; set; }

    public virtual Track? Track { get; set; }

    public virtual User? User { get; set; }
}
