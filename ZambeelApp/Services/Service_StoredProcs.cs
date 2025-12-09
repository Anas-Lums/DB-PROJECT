using Microsoft.EntityFrameworkCore;
using ZambeelApp.Models;
using System.Data;

namespace ZambeelApp.Services
{
    public class Service_StoredProcs : IZambeelService
    {
        private readonly ZambeelPlusContext _context;

        public Service_StoredProcs(ZambeelPlusContext context)
        {
            _context = context;
        }

        public string RegisterStudent(string email, string name, int schoolId, int deptId, int yearEnroll)
        {
            try
            {
                // Calls your SQL SP: sp_RegisterNewStudent
                _context.Database.ExecuteSqlRaw(
                    "EXEC sp_RegisterNewStudent @p0, @p1, @p2, @p3, @p4", 
                    email, name, schoolId, deptId, yearEnroll
                );
                return "Success: Student Registered via Stored Procedure.";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public string EnrollStudent(int studentId, int courseId, string semester)
        {
            try
            {
                // Calls your SQL SP: sp_EnrollStudent
                _context.Database.ExecuteSqlRaw(
                    "EXEC sp_EnrollStudent @p0, @p1, @p2", 
                    studentId, courseId, semester
                );
                return "Success: Student Enrolled via Stored Procedure.";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public List<FinanceReportDTO> GetPendingDefaulters()
        {
            // For reading complex SP results in EF Core, we use a raw query mapped to a temporary DTO
            // OR simpler: use the View you created "v_FinanceOverview" or similar logic if the SP is tricky.
            // Let's try raw SQL mapping.
            
            var result = new List<FinanceReportDTO>();
            
            // We'll open a raw ADO.NET connection to be safe, as EF Core mapping to non-entities can be tricky
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "EXEC sp_Report_FinanceOutstanding";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new FinanceReportDTO
                            {
                                StudentName = reader.GetString(0), // Name is col 0
                                OutstandingAmount = reader.GetDecimal(1) // Amount is col 1
                            });
                        }
                    }
                }
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
            return result;
        }

        public List<CoursesBefore> GetAllCourses()
        {
            return _context.CoursesBefores.ToList();
        }
    }
}