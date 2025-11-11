using Lms.CourseDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lms.CourseDelivery.Infrastructure.Configuration
{
    public class CourseDeliveryConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CourseDelivery> builder)
        {
            throw new NotImplementedException();
        }

        public void Configure(EntityTypeBuilder<Course> builder)
        {
            throw new NotImplementedException();
        }
    }
}
