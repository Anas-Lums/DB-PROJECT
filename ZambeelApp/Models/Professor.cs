using System;
using System.Collections.Generic;

namespace ZambeelApp.Models;

public partial class Professor
{
    public int ProfessorId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int? DepartmentId { get; set; }

    public virtual ICollection<CoursesBefore> CoursesBefores { get; set; } = new List<CoursesBefore>();

    public virtual Department? Department { get; set; }

    public virtual ICollection<Timetable> Timetables { get; set; } = new List<Timetable>();
}
