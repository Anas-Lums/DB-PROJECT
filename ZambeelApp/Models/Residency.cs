using System;
using System.Collections.Generic;

namespace ZambeelApp.Models;

public partial class Residency
{
    public int HostelId { get; set; }

    public int StudentId { get; set; }

    public string HostelName { get; set; } = null!;

    public string? RoomNumber { get; set; }

    public bool? IsPaid { get; set; }

    public virtual Student Student { get; set; } = null!;
}
