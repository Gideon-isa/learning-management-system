using Lms.ContentDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lms.ContentDelivery.Infrastructure.Configuration
{
    public class CourseModuleConfiguration : IEntityTypeConfiguration<CourseModuleContent>
    {
        public void Configure(EntityTypeBuilder<CourseModuleContent> builder)
        {
            builder.HasKey(cm => cm.Id);

            builder.Property(m => m.Id)
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd();


            builder.HasMany<LessonContent>(cm => cm.Lessons)
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
