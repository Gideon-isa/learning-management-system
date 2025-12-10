using Lms.CourseManagement.Application.Models;
using Lms.CourseManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lms.CourseManagement.Infrastructure.DbContex
{
    public class CourseManagementDbContext : DbContext
    {
        public CourseManagementDbContext(DbContextOptions<CourseManagementDbContext> options) : base(options)
        { 
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all configurations from the current assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseManagementDbContext).Assembly);

            // This configures EF to populate the backing field instead of the Public field
            // e.g "_modules" in Course Entity should be used to pupulate and not "Modules".
            // Since Module is a IReadOnlyCollection.
            modelBuilder.Entity<Course>().Navigation(c => c.Modules)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Entity<CourseModule>().Navigation(c => c.Lessons)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Entity<Content>().Navigation(c => c.Videos)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Entity<Content>().Navigation(c => c.Images)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Entity<Content>().Navigation(c => c.Notes)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseModule> CourseModules { get; set; }
        public DbSet<Content> Lessons { get; set; }
        public DbSet<LessonTag> LessonTags { get; set; }
        public DbSet<Instructor> Instructors  => Set<Instructor>();
        public DbSet<CourseCategory> CourseCategories  => Set<CourseCategory>();
    }
}
