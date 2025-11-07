using Lms.Enrollment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lms.Enrollment.Infrastructure.Configuration
{
    public class CourseEnrollmentConfiguration : IEntityTypeConfiguration<CourseEnrollment>
    {
        public void Configure(EntityTypeBuilder<CourseEnrollment> builder)
        {
            builder.HasKey(c => c.Id);
            builder.OwnsMany(c => c.StudentEnrollments, b =>
            {
                b.WithOwner()
                .HasForeignKey("CourseEnrollmentId");

                b.Property<Guid>("Id");
                b.HasKey("Id");

                b.Property(e => e.StudentId).IsRequired();
                b.Property(e => e.StudentName).HasMaxLength(200).IsRequired();
                b.Property(e => e.EnrolledOn).IsRequired();
                b.Property(e => e.Status).IsRequired();

                b.ToTable("StudentEnrollments");

            });
        }
    }
}
