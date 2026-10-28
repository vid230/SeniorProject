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

            int remainingCredits = Math.Max(0, requiredCredits - earnedCredits);

            double progressPercentage = (requiredCredits == 0) ? 0 : Math.Min(100, (double)earnedCredits / requiredCredits * 100);

            return new DegreeProgressResult
            {
                RequiredCredits = requiredCredits,
                EarnedCredits = earnedCredits,
                RemainingCredits = remainingCredits,
                ProgressPercentage = progressPercentage
            };
        }
    }
}