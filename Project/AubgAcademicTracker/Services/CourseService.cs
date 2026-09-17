using AubgAcademicTracker.Data;
using AubgAcademicTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace AubgAcademicTracker.Services
{
    public class CourseService
    {
        private readonly IDbContextFactory<AcademicDbContext> contextFactory;

        public CourseService(IDbContextFactory<AcademicDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<List<Course>> GetCoursesAsync()
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            return await context.Courses.AsNoTracking().OrderBy(course => course.Id).ToListAsync();
        }

        public async Task AddCourseAsync(Course course)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            context.Courses.Add(course);

            await context.SaveChangesAsync();
        }
    }
}