using AubgAcademicTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace AubgAcademicTracker.Data
{
    public class AcademicDbContext : DbContext
    {
        public AcademicDbContext(DbContextOptions<AcademicDbContext> options) : base(options) {}

        public DbSet<Course> Courses => Set<Course>();

        public DbSet<Semester> Semesters => Set<Semester>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Semester>().HasMany(semester => semester.Courses).WithOne(course => course.Semester).HasForeignKey(course => course.SemesterId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Semester>().HasIndex(semester => new
            {
                semester.Term,
                semester.Year
            }).IsUnique();
        }
    }
}