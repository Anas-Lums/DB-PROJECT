using System;
using System.Collections.Generic;

namespace ZambeelApp.Models;

public partial class VGradeSummary
{
    public string Student { get; set; } = null!;

    public string? Course { get; set; }

    public string? LetterGrade { get; set; }

    public decimal? ExamMarks { get; set; }

    public decimal? ProjectMarks { get; set; }
}
