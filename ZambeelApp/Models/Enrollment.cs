using System;
using System.Collections.Generic;

namespace ZambeelApp.Models;

public partial class Enrollment
{
    public int EnrollmentId { get; set; }

    public int CourseId { get; set; }

    public int StudentId { get; set; }

    public int? Credits { get; set; }

    public string? Grade { get; set; }

    public string? Status { get; set; }

    public string? Semester { get; set; }

    public virtual CoursesBefore Course { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
