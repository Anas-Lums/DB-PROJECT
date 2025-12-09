using System;
using System.Collections.Generic;

namespace ZambeelApp.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int? YearOfEnrollment { get; set; }

    public int? YearOfGraduation { get; set; }

    public int? SchoolId { get; set; }

    public int? DepartmentId { get; set; }

    public string? Address { get; set; }

    public string? Hostel { get; set; }

    public string? RoomNumber { get; set; }

    public decimal? Gpa { get; set; }

    public int? TotalCh { get; set; }

    public string? CurrentYear { get; set; }

    public bool? FinanceStatus { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<FinalSchedule> FinalSchedules { get; set; } = new List<FinalSchedule>();

    public virtual ICollection<Finance> Finances { get; set; } = new List<Finance>();

    public virtual ICollection<Gradebook> Gradebooks { get; set; } = new List<Gradebook>();

    public virtual ICollection<Residency> Residencies { get; set; } = new List<Residency>();

    public virtual School? School { get; set; }
}
