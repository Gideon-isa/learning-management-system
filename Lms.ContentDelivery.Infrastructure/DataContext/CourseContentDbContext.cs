using Lms.ContentDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lms.ContentDelivery.Infrastructure.DataContext
{
    public class CourseContentDbContext : DbContext
    {
        public CourseContentDbContext(DbContextOptions<CourseContentDbContext> options) : base (options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseContentDbContext).Assembly);

            modelBuilder.Entity<CourseContent>().Navigation(c => c.Modules)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Entity<CourseModuleContent>().Navigation(c => c.Lessons)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Entity<LessonContent>().Navigation(c => c.Videos)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }

        public DbSet<CourseContent> CourseContents => Set<CourseContent>();
        public DbSet<CourseModuleContent> CourseModuleContents => Set<CourseModuleContent>();
        public DbSet<LessonContent> LessonContents => Set<LessonContent>();
        public DbSet<StudentAccess> StudentAccesses => Set<StudentAccess>();
    }
}
