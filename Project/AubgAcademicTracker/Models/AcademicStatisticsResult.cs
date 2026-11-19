namespace AubgAcademicTracker.Models
{
    public class AcademicStatisticsResult
    {
        public List<SemesterStatistics> Semesters { get; set; } = new();

        public List<GradeStatistics> GradeDistribution { get; set; } = new();

        public double HighestSemesterGpa { get; set; }

        public double LowestSemesterGpa { get; set; }

        public double AverageSemesterGpa { get; set; }

        public int TotalCourses { get; set; }
    }
}