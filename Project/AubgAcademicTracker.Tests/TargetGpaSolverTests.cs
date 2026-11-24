using AubgAcademicTracker.Models;
using AubgAcademicTracker.Services;

namespace AubgAcademicTracker.Tests
{
    public class TargetGpaSolverTests
    {
        private TargetGpaSolver CreateSolver()
        {
            var creditCalculator = new CreditCalculator();
            var gpaCalculator = new GpaCalculator(creditCalculator);

            return new TargetGpaSolver(gpaCalculator, creditCalculator);
        }

        [Fact]
        public void Solve_WithReachableTarget_ReturnsAchievableResult()
        {
            // Arrange
            var solver = CreateSolver();

            var currentCourses = new List<Course>
            {
                new Course
                {
                    Code = "COS100",
                    Name = "Current Course",
                    Credits = 3,
                    Grade = "A"
                }
            };

            var remainingCourses = new List<RemainingCourse>
            {
                new RemainingCourse
                {
                    Name = "Future Course",
                    Credits = 3
                }
            };

            // Act
            TargetGpaResult result = solver.Solve(currentCourses, remainingCourses, 3.50);

            // Assert
            Assert.True(result.IsAchievable);
            Assert.True(result.ProjectedGpa >= 3.50);
        }

        [Fact]
        public void Solve_WithImpossibleTarget_ReturnsNotAchievable()
        {
            // Arrange
            var solver = CreateSolver();

            var currentCourses = new List<Course>
            {
                new Course
                {
                    Code = "COS100",
                    Name = "Current Course",
                    Credits = 3,
                    Grade = "B"
                }
            };

            var remainingCourses = new List<RemainingCourse>
            {
                new RemainingCourse
                {
                    Name = "Future Course",
                    Credits = 3
                }
            };

            // Act
            TargetGpaResult result = solver.Solve(currentCourses, remainingCourses, 4.00);

            // Assert
            Assert.False(result.IsAchievable);
        }

        [Fact]
        public void Solve_WithTargetAboveFour_ThrowsException()
        {
            // Arrange
            var solver = CreateSolver();

            var currentCourses = new List<Course>();
            var remainingCourses = new List<RemainingCourse>();

            // Act + Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => solver.Solve(currentCourses, remainingCourses, 4.01));
        }

        [Fact]
        public void Solve_WithNegativeTarget_ThrowsException()
        {
            // Arrange
            var solver = CreateSolver();

            var currentCourses = new List<Course>();
            var remainingCourses = new List<RemainingCourse>();

            // Act + Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => solver.Solve(currentCourses, remainingCourses, -0.01));
        }
    }
}