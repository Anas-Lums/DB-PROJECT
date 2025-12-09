using System.Data;
using ZambeelApp.Models; // Maps to your scaffolded classes

namespace ZambeelApp.Services
{
    // The Contract: Both BLLs (LINQ and Stored Proc) must implement this.
    public interface IZambeelService
    {
        // 1. Register Student
        string RegisterStudent(string email, string name, int schoolId, int deptId, int yearEnroll);

        // 2. Enroll Student
        string EnrollStudent(int studentId, int courseId, string semester);

        // 3. Get Pending Fees (Reporting) - We use a simple list of objects/DTOs here
        List<FinanceReportDTO> GetPendingDefaulters();

        // Helper to populate dropdowns
        List<CoursesBefore> GetAllCourses();
    }

    // A simple helper class to hold report data
    public class FinanceReportDTO
    {
        public string? StudentName { get; set; }
        public decimal OutstandingAmount { get; set; }
    }
}