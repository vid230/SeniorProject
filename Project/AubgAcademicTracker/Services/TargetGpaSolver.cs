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
            if ()
        }
    }
}
