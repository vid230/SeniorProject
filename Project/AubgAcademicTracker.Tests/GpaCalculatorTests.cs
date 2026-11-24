using AubgAcademicTracker.Models;
using AubgAcademicTracker.Services;

namespace AubgAcademicTracker.Tests
{
    public class GpaCalculatorTests
    {
        [Fact]
        public void CalculateGpa_WithTwoCourses_ReturnsCorrectGpa()
        {
            // Arrange
            var creditCalculator = new CreditCalculator();
            var calculator = new GpaCalculator(creditCalculator);

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
                    Grade = "B+"
                }
            };

            // Act
            double result = calculator.CalculateGpa(courses);

            // Assert
            Assert.Equal(3.65, result, 2);
        }
    }
}