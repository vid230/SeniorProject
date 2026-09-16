using AubgAcademicTracker.Models;

namespace AubgAcademicTracker.Services
{
    public class CourseService
    {
        private readonly List<Course> courses = new();

        private int nextId = 1;

        public IReadOnlyList<Course> Courses => courses;

        public void AddCourse(Course course)
        {
            course.Id = nextId++;

            courses.Add(course);
        }
    }
}
