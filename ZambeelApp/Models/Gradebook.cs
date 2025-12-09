using System;
using System.Collections.Generic;

namespace ZambeelApp.Models;

public partial class Gradebook
{
    public int GradebookId { get; set; }

    public int StudentId { get; set; }

    public int CourseId { get; set; }

    public decimal? ExamMarks { get; set; }

    public decimal? QuizMarks { get; set; }

    public decimal? AssignmentMarks { get; set; }

    public decimal? AttendancePercent { get; set; }

    public decimal? ProjectMarks { get; set; }

    public string? LetterGrade { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual CoursesBefore Course { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
