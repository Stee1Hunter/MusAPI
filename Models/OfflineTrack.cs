using System;
using System.Collections.Generic;

namespace MusAPI.Models;

public partial class OfflineTrack
{
    public int OfflineId { get; set; }

    public int? UserId { get; set; }

    public int? TrackId { get; set; }

    public DateTime? DownloadedAt { get; set; }

    public string OfflinePath { get; set; } = null!;

    public virtual Track? Track { get; set; }

    public virtual User? User { get; set; }
}
