using Lms.ContentDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lms.ContentDelivery.Infrastructure.Configuration
{
    public class StudentAccessConfiguration : IEntityTypeConfiguration<StudentAccess>
    {
        public void Configure(EntityTypeBuilder<StudentAccess> builder)
        {


            builder.HasKey(s => s.Id);

            builder.HasIndex(s => new { s.StudentCode, s.CourseId }).IsUnique();

            builder.Property(s => s.GrantedAt).IsRequired();
            builder.Property(s => s.IsRevoked).IsRequired();
        }
    }
}
