using System;
using System.Collections.Generic;

namespace ZambeelApp.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public string MajorName { get; set; } = null!;

    public int SchoolId { get; set; }

    public int? EnrollmentCount { get; set; }

    public virtual ICollection<CoursesBefore> CoursesBefores { get; set; } = new List<CoursesBefore>();

    public virtual ICollection<Professor> Professors { get; set; } = new List<Professor>();

    public virtual School School { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
