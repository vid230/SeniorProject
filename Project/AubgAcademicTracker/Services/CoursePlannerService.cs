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

                if (!courseDictionary.ContainsKey(course))
                {
                    result.Message = $"Course '{course}' does not exist in the plan.";

                    return result;
                }

                if (string.Equals(prerequisite, course, StringComparison.OrdinalIgnoreCase))
                {
                    result.Message = "A course cannot be its own prerequisite.";

                    return result;
                }

                // HashSet prevents duplicate edges.
                if (graph[prerequisite].Add(course))
                {
                    inDegree[course]++;
                    edgeCount++;
                }
            }

            Queue<string> availableCourses = new();

            foreach (var pair in inDegree)
            {
                if (pair.Value == 0)
                {
                    availableCourses.Enqueue(pair.Key);
                }
            }

            List<PlannedCourse> orderedCourses = new();

            // KAHN'S TOPOLOGICAL SORT
            while (availableCourses.Count > 0)
            {
                string current = availableCourses.Dequeue();

                orderedCourses.Add(courseDictionary[current]);

                foreach (string dependentCourse in graph[current])
                {
                    inDegree[dependentCourse]--;

                    if (inDegree[dependentCourse] == 0)
                    {
                        availableCourses.Enqueue(dependentCourse);
                    }
                }
            }

            result.VertexCount = courseDictionary.Count;

            result.EdgeCount = edgeCount;

            // If not every vertex was processed,
            // a directed cycle exists.
            if (orderedCourses.Count != courseDictionary.Count)
            {
                result.HasCycle = true;

                result.Message = "A prerequisite cycle was detected. No valid course order exists.";

                return result;
            }

            result.Success = true;
        }
    }
}
