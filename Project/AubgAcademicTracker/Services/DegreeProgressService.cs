using AubgAcademicTracker.Models;

namespace AubgAcademicTracker.Services
{
    public class DegreeProgressService
    {
        private readonly CreditCalculator creditCalculator;

        public DegreeProgressService(CreditCalculator creditCalculator)
        {
            this.creditCalculator = creditCalculator;
        }

        public DegreeProgressResult CalculateProgress(IEnumerable<Course> courses, int requiredCredits = 120)
        {
            int earnedCredits = creditCalculator.CalculateEarnedCredits(courses);
        }
    }
}
