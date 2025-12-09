using System;
using System.Collections.Generic;

namespace ZambeelApp.Models;

public partial class Assignment
{
    public int AssignmentId { get; set; }

    public string? Name { get; set; }

    public int GradebookId { get; set; }

    public int StudentId { get; set; }

    public DateOnly? OpenDate { get; set; }

    public DateOnly? DueDate { get; set; }

    public int? Marks { get; set; }

    public string? ManualLink { get; set; }

    public string? SubmissionLink { get; set; }

    public virtual Gradebook Gradebook { get; set; } = null!;
}
