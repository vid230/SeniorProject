using AubgAcademicTracker.Models;
using AubgAcademicTracker.Services;

namespace AubgAcademicTracker.Tests
{
    public class DegreeProgressServiceTests
    {
        [Fact]
        public void CalculateProgress_WithPassedAndFailedCourses_ReturnsCorrectCredits()
        {
            // Arrange
            var creditCalculator = new CreditCalculator();
            var service = new DegreeProgressService(creditCalculator);

            var courses = new List<Course>
            {
                new Course
                {
                    Code = "COS100",
                    Name = "Passed Course",
                    Credits = 3,
                    Grade = "A"
                },

                new Course
                {
                    Code = "COS200",
                    Name = "Failed Course",
                    Credits = 4,
                    Grade = "F"
                }
            };

            // Act
            DegreeProgressResult result = service.CalculateProgress(courses, 120);

            // Assert
            Assert.Equal(120, result.RequiredCredits);
            Assert.Equal(3, result.EarnedCredits);
            Assert.Equal(117, result.RemainingCredits);
            Assert.Equal(2.5, result.ProgressPercentage, 2);
        }

        [Fact]
        public void CalculateProgress_WhenCreditsExceedRequirement_CapsProgressAtOneHundred()
        {
            // Arrange
            var creditCalculator = new CreditCalculator();
            var service = new DegreeProgressService(creditCalculator);

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
                }
            };

            // Act
            DegreeProgressResult result = service.CalculateProgress(courses, 5);

            // Assert
            Assert.Equal(6, result.EarnedCredits);
            Assert.Equal(0, result.RemainingCredits);
            Assert.Equal(100, result.ProgressPercentage);
        }

        [Fact]
        public void CalculateProgress_WithNoCourses_ReturnsZeroProgress()
        {
            // Arrange
            var creditCalculator = new CreditCalculator();
            var service = new DegreeProgressService(creditCalculator);

            var courses = new List<Course>();

            // Act
            DegreeProgressResult result = service.CalculateProgress(courses, 120);

            // Assert
            Assert.Equal(0, result.EarnedCredits);
            Assert.Equal(120, result.RemainingCredits);
            Assert.Equal(0, result.ProgressPercentage);
        }
    }
}