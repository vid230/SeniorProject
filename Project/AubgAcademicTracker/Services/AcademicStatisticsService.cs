using AubgAcademicTracker.Models;

namespace AubgAcademicTracker.Services
{
    public class AcademicStatisticsService
    {
        private readonly GpaCalculator gpaCalculator;
        private readonly CreditCalculator creditCalculator;

        private readonly string[] gradeOrder =
        {
            "A",
            "A-",
            "B+",
            "B",
            "B-",
            "C+",
            "C",
            "C-",
            "D+",
            "D",
            "F"
        };

        public AcademicStatisticsService(GpaCalculator gpaCalculator, CreditCalculator creditCalculator)
        {
            this.gpaCalculator = gpaCalculator;
            this.creditCalculator = creditCalculator;
        }

        public AcademicStatisticsResult CalculateStatistics(IEnumerable<Semester> semesters, IEnumerable<Course> courses)
        {
            List<Semester> semesterList = semesters.ToList();

            List<Course> courseList = courses.ToList();

            AcademicStatisticsResult result = new();

            foreach (Semester semester in semesterList)
            {
                List<Course> semesterCourses = semester.Courses.ToList();

                if (semesterCourses.Count == 0)
                {
                    continue;
                }

                result.Semesters.Add(
                    new SemesterStatistics
                    {
                        SemesterName = semester.DisplayName,

                        Gpa = gpaCalculator.CalculateGpa(semesterCourses),

                        AttemptedCredits = creditCalculator.CalculateAttemptedCredits(semesterCourses),

                        EarnedCredits = creditCalculator.CalculateEarnedCredits(semesterCourses),

                        CourseCount = semesterCourses.Count
                    });
            }

            foreach (string grade in gradeOrder)
            {
                int count = courseList.Count(course => course.Grade == grade);

                result.GradeDistribution.Add(
                    new GradeStatistics
                    {
                        Grade = grade,
                        Count = count
                    });
            }

            result.TotalCourses = courseList.Count;

            if (result.Semesters.Count > 0)
            {
                result.HighestSemesterGpa = result.Semesters.Max(semester => semester.Gpa);

                result.LowestSemesterGpa = result.Semesters.Min(semester => semester.Gpa);

                result.AverageSemesterGpa = result.Semesters.Average(semester => semester.Gpa);
            }

            return result;
        }
    }
}