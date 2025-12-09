using System;
using System.Collections.Generic;

namespace ZambeelApp.Models;

public partial class VMajorPerformanceCte
{
    public string MajorName { get; set; } = null!;

    public string? Semester { get; set; }

    public int? TotalStudents { get; set; }

    public decimal? AverageGpa { get; set; }
}
