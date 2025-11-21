using Lms.Enrollment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lms.Enrollment.Infrastructure.DataContext
{
    public class EnrollmentSupportDbContext : DbContext
    {
        public EnrollmentSupportDbContext(DbContextOptions<EnrollmentSupportDbContext> options) : base(options) { }

        public DbSet<EnrollmentOutboxMessage> EnrollmentOutboxMessages => Set<EnrollmentOutboxMessage>();

    }
}
