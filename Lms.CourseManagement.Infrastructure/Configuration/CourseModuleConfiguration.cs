using Lms.CourseManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lms.CourseManagement.Infrastructure.Configuration
{
    public class CourseModuleConfiguration : IEntityTypeConfiguration<CourseModule>
    {
        public void Configure(EntityTypeBuilder<CourseModule> builder)
        {
            builder.HasKey(cm => cm.Id);

            builder.Property(m => m.Id)
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd();

            builder.HasMany<Content>(cm => cm.Lessons)
                .WithOne()
                .HasForeignKey("ModuleId")
                .OnDelete(DeleteBehavior.Cascade); // shadow FK

            builder.Property(cm => cm.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(cm => cm.Description)
                .HasMaxLength(200);
        }
    }
}
