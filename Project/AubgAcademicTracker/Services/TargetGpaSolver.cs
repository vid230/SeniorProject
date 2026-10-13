using AubgAcademicTracker.Models;

namespace AubgAcademicTracker.Services
{
    public class TargetGpaSolver
    {
        private readonly GpaCalculator gpaCalculator;
        private readonly CreditCalculator creditCalculator;

        private readonly string[] possibleGrades =
        {
            "D",
            "D+",
            "C-",
            "C",
            "C+",
            "B-",
            "B",
            "B+",
            "A-",
            "A"
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

            // Higher-credit courses first generally make the bounds stronger earlier in the search.
            List<RemainingCourse> remainingCourseList = remainingCourses.OrderByDescending(course => course.Credits).ToList();

            int existingCredits = creditCalculator.CalculateAttemptedCredits(currentCourseList);

            int remainingCredits = remainingCourseList.Sum(course => course.Credits);

            int finalCredits = existingCredits + remainingCredits;

            TargetGpaResult result = new();

            if (finalCredits == 0)
            {
                return result;
            }

            // Grade points are multiplied by 10 so that search
            // states use integers rather than floating-point values.
            int existingPoints = currentCourseList.Sum(course => ToScaledGradePoints(course.Grade) * course.Credits);

            int requiredTotalPoints = (int) Math.Ceiling(targetGpa * finalCredits * 10.0 - 0.0000001);

            int requiredNewPoints = requiredTotalPoints - existingPoints;

            int courseCount = remainingCourseList.Count;

            int[] minimumPointsFromIndex = new int[courseCount + 1];

            int[] maximumPointsFromIndex = new int[courseCount + 1];

            int minimumGradePoints = ToScaledGradePoints("D");

            int maximumGradePoints = ToScaledGradePoints("A");

            for ()
            {
                //
            }
        }
    }
}