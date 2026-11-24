using AubgAcademicTracker.Models;
using AubgAcademicTracker.Services;

namespace AubgAcademicTracker.Tests
{
    public class AcademicStatisticsServiceTests
    {
        private AcademicStatisticsService CreateService()
        {
            var creditCalculator = new CreditCalculator();

            var gpaCalculator = new GpaCalculator(creditCalculator);

            return new AcademicStatisticsService(gpaCalculator, creditCalculator);
        }

        [Fact]
        public void CalculateStatistics_WithSemesterCourses_ReturnsCorrectSemesterGpa()
        {
            // Arrange
            var service = CreateService();

            var course1 = new Course
            {
                Code = "COS100",
                Name = "Course 1",
                Credits = 3,
                Grade = "A"
            };

            var course2 = new Course
            {
                Code = "COS200",
                Name = "Course 2",
                Credits = 3,
                Grade = "B+"
            };

            var semester = new Semester
            {
                Term = "Fall",
                Year = 2026,
                Courses = new List<Course>
                {
                    course1,
                    course2
                }
            };

            var semesters = new List<Semester> { semester };

            var courses = new List<Course>
                {
                    course1,
                    course2
                };

            // Act
            AcademicStatisticsResult result = service.CalculateStatistics(semesters, courses);

            // Assert
            Assert.Single(result.Semesters);

            SemesterStatistics semesterResult = result.Semesters[0];

            Assert.Equal(3.65, semesterResult.Gpa, 2);
            Assert.Equal(6, semesterResult.AttemptedCredits);
            Assert.Equal(6, semesterResult.EarnedCredits);
            Assert.Equal(2, semesterResult.CourseCount);

            Assert.Equal(2, result.TotalCourses);
        }

        [Fact]
        public void CalculateStatistics_ReturnsCorrectGradeDistribution()
        {
            // Arrange
            var service = CreateService();

            var courses = new List<Course>
            {
                new Course
                {
                    Code = "COS100",
                    Name = "Course 1",
                    Credits = 3,
                    Grade = "A"
                },

                new Course
                {
                    Code = "COS200",
                    Name = "Course 2",
                    Credits = 3,
                    Grade = "A"
                },

                new Course
                {
                    Code = "COS300",
                    Name = "Course 3",
                    Credits = 3,
                    Grade = "F"
                }
            };

            var semesters = new List<Semester>();

            // Act
            AcademicStatisticsResult result = service.CalculateStatistics(semesters, courses);

            // Assert
            GradeStatistics aStatistics = result.GradeDistribution.Single(item => item.Grade == "A");

            GradeStatistics fStatistics = result.GradeDistribution.Single(item => item.Grade == "F");

            Assert.Equal(2, aStatistics.Count);
            Assert.Equal(1, fStatistics.Count);
            Assert.Equal(3, result.TotalCourses);
        }

        [Fact]
        public void CalculateStatistics_WithNoData_ReturnsEmptyStatistics()
        {
            // Arrange
            var service = CreateService();

            var semesters = new List<Semester>();

            var courses = new List<Course>();

            // Act
            AcademicStatisticsResult result = service.CalculateStatistics(semesters, courses);

            // Assert
            Assert.Empty(result.Semesters);
            Assert.Equal(0, result.TotalCourses);
            Assert.Equal(0, result.HighestSemesterGpa);
            Assert.Equal(0, result.LowestSemesterGpa);
            Assert.Equal(0, result.AverageSemesterGpa);
        }
    }
}