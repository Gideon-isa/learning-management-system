using Lms.ContentDelivery.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Lms.ContentDelivery.Infrastructure.DataContext
{
    public class CourseContentSupportDbContext : DbContext
    {
        public CourseContentSupportDbContext(DbContextOptions<CourseContentSupportDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VideoMetadataModel>().ToTable("CourseManagementVideoMetadata");

            //Prevent EF from modifying VideoMetadats schema already generated in the CourseManagement Module
            //For reading video content stored in that table
            modelBuilder.Entity<VideoMetadataModel>().Metadata.SetIsTableExcludedFromMigrations(true); // Prevents EF Core from creating the table
        }

        public DbSet<VideoMetadataModel> VideoMetadata => Set<VideoMetadataModel>();
    }
}
