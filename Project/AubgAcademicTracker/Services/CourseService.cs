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

            return await context.Courses.Include(course => course.Semester).AsNoTracking().OrderBy(course => course.Id).ToListAsync();
        }

        public async Task AddCourseAsync(Course course)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            context.Courses.Add(course);

            await context.SaveChangesAsync();
        }

        public async Task<Course?> GetCourseByIdAsync(int id)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            return await context.Courses.Include(course => course.Semester).AsNoTracking().FirstOrDefaultAsync(course => course.Id == id);
        }

        public async Task UpdateCourseAsync(Course course)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            context.Courses.Update(course);

            await context.SaveChangesAsync();
        }

        public async Task DeleteCourseAsync(int id)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            Course? course = await context.Courses.FindAsync(id);

            if (course != null)
            {
                context.Courses.Remove(course);
                await context.SaveChangesAsync();
            }
        }
    }
}