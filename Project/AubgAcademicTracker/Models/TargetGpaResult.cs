namespace AubgAcademicTracker.Models
{
    public class TargetGpaResult
    {
        public bool IsAchievable { get; set; }
        public double ProjectedGpa { get; set; }
        public List<GradeRecommendation> Recommendations { get; set; } = new();
        public long NodeVisited { get; set; }
        public long BranchesPruned { get; set; }
    }
}
