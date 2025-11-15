using Lms.CourseManagement.Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Lms.CourseManagement.Infrastructure.DbContex
{
    public class CourseSupportDbContext : DbContext
    {
        public CourseSupportDbContext(DbContextOptions<CourseSupportDbContext> options) : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);    
        }

        public DbSet<CourseOutboxMessage> CourseOutboxMessages => Set<CourseOutboxMessage>();
        public DbSet<VideoMetadata> CourseManagementVideoMetadata => Set<VideoMetadata>();
    }
}
