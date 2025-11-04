using Lms.CourseManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lms.CourseManagement.Infrastructure.Configuration
{
    public class LessonTagConfiguration : IEntityTypeConfiguration<LessonTag>
    {
        public void Configure(EntityTypeBuilder<LessonTag> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.TagName)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(e => e.TagName)
                .IsUnique();
        }
    }
}
