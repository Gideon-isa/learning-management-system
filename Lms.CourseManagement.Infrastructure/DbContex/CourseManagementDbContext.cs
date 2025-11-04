using Lms.CourseManagement.Domain.Entities;
using Lms.CourseManagement.Infrastructure.Outbox;
using Lms.SharedKernel.Application;
using Lms.SharedKernel.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

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

            modelBuilder.Entity<Course>().Navigation(c => c.Modules)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Entity<CourseModule>().Navigation(c => c.Lessons)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Entity<Lesson>().Navigation(c => c.Videos)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Entity<Lesson>().Navigation(c => c.Images)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Entity<Lesson>().Navigation(c => c.Notes)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseModule> CourseModules { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonTag> LessonTags { get; set; }
        public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

        public DbSet<Instructor> Instructors  => Set<Instructor>();
    }
}
