using System;
using System.Collections.Generic;

namespace ZambeelApp.Models;

public partial class School
{
    public int SchoolId { get; set; }

    public string Name { get; set; } = null!;

    public int? EnrollmentCount { get; set; }

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
