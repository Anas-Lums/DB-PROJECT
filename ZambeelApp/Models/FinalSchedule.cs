using System;
using System.Collections.Generic;

namespace ZambeelApp.Models;

public partial class FinalSchedule
{
    public int ScheduleId { get; set; }

    public int StudentId { get; set; }

    public int? CourseId { get; set; }

    public DateOnly? ExamDate { get; set; }

    public TimeOnly? ExamTime { get; set; }

    public virtual CoursesBefore? Course { get; set; }

    public virtual Student Student { get; set; } = null!;
}
