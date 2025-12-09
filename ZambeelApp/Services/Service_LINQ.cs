using Microsoft.EntityFrameworkCore;
using ZambeelApp.Models;

namespace ZambeelApp.Services
{
    public class Service_LINQ : IZambeelService
    {
        private readonly ZambeelPlusContext _context;

        public Service_LINQ(ZambeelPlusContext context)
        {
            _context = context;
        }

        public string RegisterStudent(string email, string name, int schoolId, int deptId, int yearEnroll)
        {
            try
            {
                // 1. Calculate Graduation Year
                int gradYear = yearEnroll + 4;

                // 2. Generate ID Logic (Simplified C# version of your SQL logic)
                string yearPrefix = gradYear.ToString().Substring(2, 2);
                string schoolPrefix = schoolId.ToString("D2");
                string prefix = yearPrefix + schoolPrefix;

                // Find max existing ID with this prefix
                int minId = int.Parse(prefix + "0000");
                int maxIdRange = int.Parse(prefix + "9999");

                var maxExisting = _context.Students
                    .Where(s => s.StudentId >= minId && s.StudentId <= maxIdRange)
                    .Max(s => (int?)s.StudentId);

                int nextSeq = 1;
                if (maxExisting.HasValue)
                {
                    nextSeq = int.Parse(maxExisting.Value.ToString().Substring(4)) + 1;
                }

                int newId = int.Parse(prefix + nextSeq.ToString("D4"));

                // 3. Insert
                var student = new Student
                {
                    StudentId = newId,
                    Email = email,
                    Name = name,
                    YearOfEnrollment = yearEnroll,
                    YearOfGraduation = gradYear,
                    SchoolId = schoolId,
                    DepartmentId = deptId,
                    FinanceStatus = true
                };

                _context.Students.Add(student);
                _context.SaveChanges();

                return $"Success (LINQ): Registered {name} with ID {newId}";
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
                // 1. Get Course Info
                var course = _context.CoursesBefores.Find(courseId);
                if (course == null) return "Course not found";

                // 2. Create Enrollment
                var enroll = new Enrollment
                {
                    StudentId = studentId,
                    CourseId = courseId,
                    Credits = course.CreditHours,
                    Status = "Enrolled",
                    Semester = semester
                };

                _context.Enrollments.Add(enroll);
                _context.SaveChanges();

                return "Success (LINQ): Enrolled successfully.";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}"; // Will catch unique constraint violations
            }
        }

        public List<FinanceReportDTO> GetPendingDefaulters()
        {
            // LINQ Logic: Join Finances and Student, filter by IsPaid = false
            var query = from f in _context.Finances
                        join s in _context.Students on f.StudentId equals s.StudentId
                        where f.IsPaid == false
                        group f by new { s.StudentId, s.Name } into g
                        select new FinanceReportDTO
                        {
                            StudentName = g.Key.Name,
                            OutstandingAmount = g.Sum(x => x.Amount) ?? 0
                        };

            return query.OrderByDescending(x => x.OutstandingAmount).ToList();
        }

        public List<CoursesBefore> GetAllCourses()
        {
            return _context.CoursesBefores.ToList();
        }
    }
}