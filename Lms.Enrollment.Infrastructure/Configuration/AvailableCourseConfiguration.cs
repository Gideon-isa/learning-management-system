using Lms.Enrollment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lms.Enrollment.Infrastructure.Configuration
{
    public class AvailableCourseConfiguration : IEntityTypeConfiguration<AvailableCourse>
    {
        public void Configure(EntityTypeBuilder<AvailableCourse> builder)
        {
            builder.HasKey(c => c.Id);  
        }
    }
}
