using Lms.CourseManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lms.CourseManagement.Infrastructure.Configuration
{
    public class CourseCategoryConfiguration : IEntityTypeConfiguration<CourseCategory>
    {
        public void Configure(EntityTypeBuilder<CourseCategory> builder)
        {
            builder.HasKey(cc => cc.Id);
            builder.Property(cc => cc.Name)
                   .IsRequired()
                   .HasMaxLength(200);
        }
    }
}
