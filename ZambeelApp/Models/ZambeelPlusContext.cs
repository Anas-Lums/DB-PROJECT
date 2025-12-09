using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ZambeelApp.Models;

public partial class ZambeelPlusContext : DbContext
{
    public ZambeelPlusContext()
    {
    }

    public ZambeelPlusContext(DbContextOptions<ZambeelPlusContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<CoursesAfter> CoursesAfters { get; set; }

    public virtual DbSet<CoursesBefore> CoursesBefores { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<FinalSchedule> FinalSchedules { get; set; }

    public virtual DbSet<Finance> Finances { get; set; }

    public virtual DbSet<Gradebook> Gradebooks { get; set; }

    public virtual DbSet<Professor> Professors { get; set; }

    public virtual DbSet<Residency> Residencies { get; set; }

    public virtual DbSet<ResourcesInstructor> ResourcesInstructors { get; set; }

    public virtual DbSet<School> Schools { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Timetable> Timetables { get; set; }

    public virtual DbSet<VFinanceOverview> VFinanceOverviews { get; set; }

    public virtual DbSet<VGradeSummary> VGradeSummaries { get; set; }

    public virtual DbSet<VMajorPerformanceCte> VMajorPerformanceCtes { get; set; }

    public virtual DbSet<VWardenRoster> VWardenRosters { get; set; }

    //     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //         => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ZambeelPlus;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("PK__Assignme__32499E5794330CD5");

            entity.Property(e => e.AssignmentId).HasColumnName("AssignmentID");
            entity.Property(e => e.GradebookId).HasColumnName("GradebookID");
            entity.Property(e => e.ManualLink).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.OpenDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.SubmissionLink).HasMaxLength(200);

            entity.HasOne(d => d.Gradebook).WithMany(p => p.Assignments)
                .HasForeignKey(d => new { d.GradebookId, d.StudentId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Assignments__70DDC3D8");
        });

        modelBuilder.Entity<CoursesAfter>(entity =>
        {
            entity.HasKey(e => e.CourseAid).HasName("PK__Courses___08A897841C2E5A5E");

            entity.ToTable("Courses_after");

            entity.Property(e => e.CourseAid)
                .ValueGeneratedNever()
                .HasColumnName("CourseAID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.NumStudents).HasDefaultValue(0);
            entity.Property(e => e.Semester).HasMaxLength(20);

            entity.HasOne(d => d.Course).WithMany(p => p.CoursesAfters)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Courses_a__Cours__4E88ABD4");
        });

        modelBuilder.Entity<CoursesBefore>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Courses___C92D718790EAE97C");

            entity.ToTable("Courses_before");

            entity.Property(e => e.CourseId)
                .ValueGeneratedNever()
                .HasColumnName("CourseID");
            entity.Property(e => e.Core).HasMaxLength(20);
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.Elective).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PreReqId).HasColumnName("PreReqID");
            entity.Property(e => e.ProfessorId).HasColumnName("ProfessorID");
            entity.Property(e => e.Timings).HasMaxLength(100);

            entity.HasOne(d => d.Department).WithMany(p => p.CoursesBefores)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Courses_b__Depar__49C3F6B7");

            entity.HasOne(d => d.PreReq).WithMany(p => p.InversePreReq)
                .HasForeignKey(d => d.PreReqId)
                .HasConstraintName("FK__Courses_b__PreRe__4AB81AF0");

            entity.HasOne(d => d.Professor).WithMany(p => p.CoursesBefores)
                .HasForeignKey(d => d.ProfessorId)
                .HasConstraintName("FK__Courses_b__Profe__48CFD27E");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BCD32B0605D");

            entity.ToTable("Department");

            entity.Property(e => e.DepartmentId)
                .ValueGeneratedNever()
                .HasColumnName("DepartmentID");
            entity.Property(e => e.DepartmentName).HasMaxLength(100);
            entity.Property(e => e.EnrollmentCount).HasDefaultValue(0);
            entity.Property(e => e.MajorName).HasMaxLength(100);
            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

            entity.HasOne(d => d.School).WithMany(p => p.Departments)
                .HasForeignKey(d => d.SchoolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Departmen__Schoo__3B75D760");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => new { e.EnrollmentId, e.StudentId }).HasName("PK__Enrollme__FC44255CD6EDF811");

            entity.ToTable("Enrollment", tb => tb.HasTrigger("trg_UpdateRoster"));

            entity.HasIndex(e => new { e.StudentId, e.CourseId, e.Semester }, "UQ_Enrollment_NoDupes").IsUnique();

            entity.Property(e => e.EnrollmentId)
                .ValueGeneratedOnAdd()
                .HasColumnName("EnrollmentID");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Grade).HasMaxLength(5);
            entity.Property(e => e.Semester).HasMaxLength(20);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Enrolled");

            entity.HasOne(d => d.Course).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Enrollmen__Cours__534D60F1");

            entity.HasOne(d => d.Student).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Enrollmen__Stude__5441852A");
        });

        modelBuilder.Entity<FinalSchedule>(entity =>
        {
            entity.HasKey(e => new { e.ScheduleId, e.StudentId }).HasName("PK__FinalSch__1FA609CEE5D6A27A");

            entity.ToTable("FinalSchedule");

            entity.Property(e => e.ScheduleId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ScheduleID");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");

            entity.HasOne(d => d.Course).WithMany(p => p.FinalSchedules)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__FinalSche__Cours__5DCAEF64");

            entity.HasOne(d => d.Student).WithMany(p => p.FinalSchedules)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FinalSche__Stude__5CD6CB2B");
        });

        modelBuilder.Entity<Finance>(entity =>
        {
            entity.HasKey(e => new { e.FinanceTransactionId, e.StudentId }).HasName("PK__Finances__C87DAE167397B8F7");

            entity.Property(e => e.FinanceTransactionId)
                .ValueGeneratedOnAdd()
                .HasColumnName("FinanceTransactionID");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.Amount)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.IsPaid).HasDefaultValue(false);
            entity.Property(e => e.IssueDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TransactionType).HasMaxLength(50);

            entity.HasOne(d => d.Student).WithMany(p => p.Finances)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Finances__Studen__7B5B524B");
        });

        modelBuilder.Entity<Gradebook>(entity =>
        {
            entity.HasKey(e => new { e.GradebookId, e.StudentId }).HasName("PK__Gradeboo__B22AA8D8CF2BDFFD");

            entity.ToTable("Gradebook", tb => tb.HasTrigger("trg_UpdateStudentGPA"));

            entity.Property(e => e.GradebookId)
                .ValueGeneratedOnAdd()
                .HasColumnName("GradebookID");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.AssignmentMarks)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(5, 2)");
            entity.Property(e => e.AttendancePercent)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(5, 2)");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.ExamMarks)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(5, 2)");
            entity.Property(e => e.LetterGrade).HasMaxLength(2);
            entity.Property(e => e.ProjectMarks)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(5, 2)");
            entity.Property(e => e.QuizMarks)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Course).WithMany(p => p.Gradebooks)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Gradebook__Cours__6B24EA82");

            entity.HasOne(d => d.Student).WithMany(p => p.Gradebooks)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Gradebook__Stude__6A30C649");
        });

        modelBuilder.Entity<Professor>(entity =>
        {
            entity.HasKey(e => e.ProfessorId).HasName("PK__Professo__90035969E385D37B");

            entity.ToTable("Professor");

            entity.HasIndex(e => e.Email, "UQ__Professo__A9D10534E560F6DA").IsUnique();

            entity.Property(e => e.ProfessorId)
                .ValueGeneratedNever()
                .HasColumnName("ProfessorID");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Department).WithMany(p => p.Professors)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Professor__Depar__45F365D3");
        });

        modelBuilder.Entity<Residency>(entity =>
        {
            entity.HasKey(e => new { e.HostelId, e.StudentId }).HasName("PK__Residenc__E452B96FE9C7F1F7");

            entity.ToTable("Residency", tb =>
                {
                    tb.HasTrigger("trg_ResidencyCleanup");
                    tb.HasTrigger("trg_ValidateRoomFormat");
                });

            entity.HasIndex(e => new { e.StudentId, e.HostelName, e.RoomNumber }, "IX_Residency_RoomSearch").HasFilter("([RoomNumber] IS NOT NULL)");

            entity.HasIndex(e => e.StudentId, "IX_Residency_Student");

            entity.Property(e => e.HostelId)
                .ValueGeneratedOnAdd()
                .HasColumnName("HostelID");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.HostelName).HasMaxLength(100);
            entity.Property(e => e.IsPaid).HasDefaultValue(false);
            entity.Property(e => e.RoomNumber).HasMaxLength(20);

            entity.HasOne(d => d.Student).WithMany(p => p.Residencies)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Residency__Stude__74AE54BC");
        });

        modelBuilder.Entity<ResourcesInstructor>(entity =>
        {
            entity.HasKey(e => e.ResourceId).HasName("PK__Resource__4ED1814FBF1748E6");

            entity.ToTable("Resources_instructor");

            entity.Property(e => e.ResourceId).HasColumnName("ResourceID");
            entity.Property(e => e.CourseAid).HasColumnName("CourseAID");
            entity.Property(e => e.Lectures).HasMaxLength(200);
            entity.Property(e => e.PracticeProblems).HasMaxLength(200);
            entity.Property(e => e.Tutorials).HasMaxLength(200);
            entity.Property(e => e.UploadDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CourseA).WithMany(p => p.ResourcesInstructors)
                .HasForeignKey(d => d.CourseAid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Resources__Cours__7F2BE32F");
        });

        modelBuilder.Entity<School>(entity =>
        {
            entity.HasKey(e => e.SchoolId).HasName("PK__School__3DA4677B79FCD488");

            entity.ToTable("School");

            entity.Property(e => e.SchoolId)
                .ValueGeneratedNever()
                .HasColumnName("SchoolID");
            entity.Property(e => e.EnrollmentCount).HasDefaultValue(0);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Student__32C52A7957DE2FB4");

            entity.ToTable("Student", tb => tb.HasTrigger("trg_AutoCreateFinanceRecord"));

            entity.HasIndex(e => e.Email, "IX_Student_Email_Unique").IsUnique();

            entity.Property(e => e.StudentId)
                .ValueGeneratedNever()
                .HasColumnName("StudentID");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.CurrentYear).HasMaxLength(20);
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FinanceStatus).HasDefaultValue(true);
            entity.Property(e => e.Gpa)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("GPA");
            entity.Property(e => e.Hostel).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.RoomNumber).HasMaxLength(20);
            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");
            entity.Property(e => e.TotalCh)
                .HasDefaultValue(0)
                .HasColumnName("TotalCH");

            entity.HasOne(d => d.Department).WithMany(p => p.Students)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Student__Departm__4222D4EF");

            entity.HasOne(d => d.School).WithMany(p => p.Students)
                .HasForeignKey(d => d.SchoolId)
                .HasConstraintName("FK__Student__SchoolI__412EB0B6");
        });

        modelBuilder.Entity<Timetable>(entity =>
        {
            entity.HasKey(e => e.TimetableId).HasName("PK__Timetabl__68413F402F0F12EF");

            entity.ToTable("Timetable");

            entity.Property(e => e.TimetableId).HasColumnName("TimetableID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Day).HasMaxLength(10);
            entity.Property(e => e.ProfessorId).HasColumnName("ProfessorID");

            entity.HasOne(d => d.Course).WithMany(p => p.Timetables)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__Timetable__Cours__59063A47");

            entity.HasOne(d => d.Professor).WithMany(p => p.Timetables)
                .HasForeignKey(d => d.ProfessorId)
                .HasConstraintName("FK__Timetable__Profe__59FA5E80");
        });

        modelBuilder.Entity<VFinanceOverview>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_FinanceOverview");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.TransactionType).HasMaxLength(50);
        });

        modelBuilder.Entity<VGradeSummary>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_GradeSummary");

            entity.Property(e => e.Course).HasMaxLength(100);
            entity.Property(e => e.ExamMarks).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.LetterGrade).HasMaxLength(2);
            entity.Property(e => e.ProjectMarks).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Student).HasMaxLength(100);
        });

        modelBuilder.Entity<VMajorPerformanceCte>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_MajorPerformanceCTE");

            entity.Property(e => e.AverageGpa)
                .HasColumnType("decimal(38, 6)")
                .HasColumnName("AverageGPA");
            entity.Property(e => e.MajorName).HasMaxLength(100);
            entity.Property(e => e.Semester).HasMaxLength(20);
        });

        modelBuilder.Entity<VWardenRoster>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_WardenRoster");

            entity.Property(e => e.HostelName).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(7)
                .IsUnicode(false);
            entity.Property(e => e.RoomNumber).HasMaxLength(20);
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
