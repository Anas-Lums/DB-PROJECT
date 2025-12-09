using System;
using System.Collections.Generic;

namespace ZambeelApp.Models;

public partial class CoursesBefore
{
    public int CourseId { get; set; }

    public string? Name { get; set; }

    public int? PreReqId { get; set; }

    public int? CreditHours { get; set; }

    public int? CourseCap { get; set; }

    public int? ProfessorId { get; set; }

    public int DepartmentId { get; set; }

    public string? Core { get; set; }

    public string? Elective { get; set; }

    public int? SectionCount { get; set; }

    public string? Timings { get; set; }

    public DateOnly? FinalExamDate { get; set; }

    public string? CourseOutline { get; set; }

    public virtual ICollection<CoursesAfter> CoursesAfters { get; set; } = new List<CoursesAfter>();

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<FinalSchedule> FinalSchedules { get; set; } = new List<FinalSchedule>();

    public virtual ICollection<Gradebook> Gradebooks { get; set; } = new List<Gradebook>();

    public virtual ICollection<CoursesBefore> InversePreReq { get; set; } = new List<CoursesBefore>();

    public virtual CoursesBefore? PreReq { get; set; }

    public virtual Professor? Professor { get; set; }

    public virtual ICollection<Timetable> Timetables { get; set; } = new List<Timetable>();
}
