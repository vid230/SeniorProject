using AubgAcademicTracker.Models;

namespace AubgAcademicTracker.Services
{
    public class CoursePlannerService
    {
        public CoursePlanResult CreatePlan(IEnumerable<PlannedCourse> courses, IEnumerable<PrerequisiteRelation> relations)
        {
            List<PlannedCourse> courseList = courses.ToList();

            List<PrerequisiteRelation> relationList = relations.ToList();

            CoursePlanResult result = new();

            if (courseList.Count == 0)
            {
                result.Message = "Add at least one planned course.";

                return result;
            }

            Dictionary<string, PlannedCourse> courseDictionary = new(StringComparer.OrdinalIgnoreCase);

            foreach (PlannedCourse course in courseList)
            {
                string code = course.Code.Trim();

                if (!courseDictionary.TryAdd(code, course))
                {
                    result.Message = $"Duplicate course code: {code}";

                    return result;
                }
            }

            // Adjacency list:
            // prerequisite -> courses that depend on it
            Dictionary<string, HashSet<string>> graph = new(StringComparer.OrdinalIgnoreCase);

            // Number of prerequisites for each course
            Dictionary<string, int> inDegree = new(StringComparer.OrdinalIgnoreCase);

            foreach (string code in courseDictionary.Keys)
            {
                graph[code] = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                inDegree[code] = 0;
            }

            int edgeCount = 0;

            foreach (PrerequisiteRelation relation in relationList)
            {
                string prerequisite = relation.PrerequisiteCode.Trim();

                string course = relation.CourseCode.Trim();

                if (!courseDictionary.ContainsKey(prerequisite))
                {
                    result.Message = $"Prerequisite course '{prerequisite}' does not exist in the plan.";

                    return result;
                }
            }
        }
    }
}
