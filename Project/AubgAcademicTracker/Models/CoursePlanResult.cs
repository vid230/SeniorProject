namespace AubgAcademicTracker.Models
{
    public class CoursePlanResult
    {
        public bool Success { get; set; }

        public bool HasCycle { get; set; }

        public string Message { get; set; } = string.Empty;

        public List<PlannedCourse> OrderedCourses { get; set; } = new();

        public int VertexCount { get; set; }

        public int EdgeCount { get; set; }
    }
}