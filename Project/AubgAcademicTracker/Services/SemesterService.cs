using AubgAcademicTracker.Data;
using AubgAcademicTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace AubgAcademicTracker.Services
{
    public class SemesterService
    {
        private readonly IDbContextFactory<AcademicDbContext> contextFactory;

        public SemesterService(IDbContextFactory<AcademicDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<List<Semester>> GetSemestersAsync()
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            return await context.Semesters.Include(semester => semester.Courses).AsNoTracking().OrderBy(semester => semester.Year).ThenBy(semester => semester.Term).ToListAsync();
        }

        public async Task<Semester?> GetSemesterByIdAsync(int id)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            return await context.Semesters.Include(semester => semester.Courses).AsNoTracking().FirstOrDefaultAsync(semester => semester.Id == id);
        }

        public async Task<bool> AddSemesterAsync(Semester semester)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            //
        }
    }
}
