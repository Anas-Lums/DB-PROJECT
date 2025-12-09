using System;
using System.Collections.Generic;

namespace ZambeelApp.Models;

public partial class VWardenRoster
{
    public string HostelName { get; set; } = null!;

    public string? RoomNumber { get; set; }

    public string Name { get; set; } = null!;

    public int StudentId { get; set; }

    public string PaymentStatus { get; set; } = null!;
}
