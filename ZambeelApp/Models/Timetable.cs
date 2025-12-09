using System;
using System.Collections.Generic;

namespace ZambeelApp.Models;

public partial class Timetable
{
    public int TimetableId { get; set; }

    public string? Day { get; set; }

    public TimeOnly? SlotStart { get; set; }

    public TimeOnly? SlotEnd { get; set; }

    public int? CourseId { get; set; }

    public int? ProfessorId { get; set; }

    public virtual CoursesBefore? Course { get; set; }

    public virtual Professor? Professor { get; set; }
}
