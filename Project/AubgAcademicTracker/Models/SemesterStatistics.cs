namespace AubgAcademicTracker.Models
{
    public class SemesterStatistics
    {
        public string SemesterName { get; set; } = string.Empty;

        public double Gpa { get; set; }

        public int AttemptedCredits { get; set; }

        public int EarnedCredits { get; set; }

        public int CourseCount { get; set; }
    }
}