using AubgAcademicTracker.Models;

namespace AubgAcademicTracker.Services
{
    public class TargetGpaSolver
    {
        private readonly GpaCalculator gpaCalculator;
        private readonly CreditCalculator creditCalculator;

        private readonly string[] possibleGrades =
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

        public TargetGpaSolver(GpaCalculator gpaCalculator, CreditCalculator creditCalculator)
        {
            this.gpaCalculator = gpaCalculator;
            this.creditCalculator = creditCalculator;
        }

        public TargetGpaResult Solve(IEnumerable<Course> currentCourses, IEnumerable<RemainingCourse> remainingCourses, double targetGpa)
        {
            if ((targetGpa < 0) || (targetGpa > 4.0))
            {
                throw new ArgumentOutOfRangeException(nameof(targetGpa), "Target GPA must be between 0.00 and 4.00.");
            }

            List<Course> currentCourseList = currentCourses.ToList();

            List<RemainingCourse> remainingCourseList = remainingCourses.ToList();

            int existingCredits = creditCalculator.CalculateAttemptedCredits(currentCourseList);

            double existingQualityPoints = currentCourseList.Sum(course => gpaCalculator.GetGradePoints(course.Grade) * course.Credits);

            int remainingCredits = remainingCourseList.Sum(course => course.Credits);

            int finalCredits = existingCredits + remainingCredits;

            TargetGpaResult result = new();

            if (finalCredits == 0)
            {
                return result;
            }

            List<GradeRecommendation> currentCombination = new();
        }
    }
}
