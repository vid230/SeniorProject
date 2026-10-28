namespace AubgAcademicTracker.Models
{
    public class DegreeProgressResult
    {
        public int RequiredCredits { get; set; }
        public int EarnedCredits { get; set; }
        public int RemainingCredits { get; set; }
        public double ProgressPercentage { get; set; }
    }
}
