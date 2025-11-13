using Lms.ContentDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lms.ContentDelivery.Infrastructure.Configuration
{
    public class CourseConfiguration : IEntityTypeConfiguration<CourseContent>
    {

        public void Configure(EntityTypeBuilder<CourseContent> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CourseTitle)
                .HasMaxLength(200);

            builder.Property(c => c.CourseCode)
                .IsRequired()
                .HasMaxLength(50);

            // Unique constriant on Instructor + Title comnbination
            builder.HasIndex(c => new { c.CourseTitle, c.InstructorId })
                .IsUnique();

            builder.HasMany<CourseModuleContent>(c => c.Modules)
                .WithOne()
                .HasForeignKey("CourseId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
