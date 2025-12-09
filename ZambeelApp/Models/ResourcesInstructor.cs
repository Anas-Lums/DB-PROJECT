using System;
using System.Collections.Generic;

namespace ZambeelApp.Models;

public partial class ResourcesInstructor
{
    public int ResourceId { get; set; }

    public int CourseAid { get; set; }

    public string? Lectures { get; set; }

    public string? Tutorials { get; set; }

    public string? PracticeProblems { get; set; }

    public DateOnly? UploadDate { get; set; }

    public virtual CoursesAfter CourseA { get; set; } = null!;
}
