using AubgAcademicTracker.Models;
using AubgAcademicTracker.Services;

namespace AubgAcademicTracker.Tests
{
    public class CreditCalculatorTests
    {
        [Fact]
        public void CalculateAttemptedCredits_IncludesAllCourses()
        {
            var calculator = new CreditCalculator();

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
                    Credits = 4,
                    Grade = "F"
                }
            };

            int result = calculator.CalculateAttemptedCredits(courses);

            Assert.Equal(7, result);
        }

        [Fact]
        public void CalculateEarnedCredits_ExcludesFailedCourses()
        {
            var calculator = new CreditCalculator();

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
                    Credits = 4,
                    Grade = "F"
                }
            };

            int result = calculator.CalculateEarnedCredits(courses);

            Assert.Equal(3, result);
        }

        [Fact]
        public void CalculateEarnedCredits_WithNoCourses_ReturnsZero()
        {
            var calculator = new CreditCalculator();

            var courses = new List<Course>();

            int result = calculator.CalculateEarnedCredits(courses);

            Assert.Equal(0, result);
        }
    }
}