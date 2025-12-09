using System;
using System.Collections.Generic;

namespace ZambeelApp.Models;

public partial class CoursesAfter
{
    public int CourseAid { get; set; }

    public int CourseId { get; set; }

    public int SectionNumber { get; set; }

    public string Semester { get; set; } = null!;

    public int? NumStudents { get; set; }

    public string? Roster { get; set; }

    public virtual CoursesBefore Course { get; set; } = null!;

    public virtual ICollection<ResourcesInstructor> ResourcesInstructors { get; set; } = new List<ResourcesInstructor>();
}
