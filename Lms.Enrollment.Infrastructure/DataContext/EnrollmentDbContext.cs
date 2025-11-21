using Lms.Enrollment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lms.Enrollment.Infrastructure.DataContext
{
    public class EnrollmentDbContext : DbContext
    {
        public EnrollmentDbContext(DbContextOptions<EnrollmentDbContext> dbContextOption) : base(dbContextOption)
        {  }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IEnrollmentInfrastructureMarker).Assembly);
        }

        public DbSet<AvailableCourse> AvailableCourses => Set<AvailableCourse>();
        public DbSet<CourseEnrollment> CourseEnrollments => Set<CourseEnrollment>();
        public DbSet<Student> Students => Set<Student>(); 
        
    }
}
