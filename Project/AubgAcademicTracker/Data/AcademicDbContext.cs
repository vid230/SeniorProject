using AubgAcademicTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace AubgAcademicTracker.Data
{
    public class AcademicDbContext : DbContext
    {
        public AcademicDbContext(
            DbContextOptions<AcademicDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses => Set<Course>();
    }
}