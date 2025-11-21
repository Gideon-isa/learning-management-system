using Lms.Shared.Abstractions.DatabaseSeeder;
using Microsoft.EntityFrameworkCore;

namespace Lms.Enrollment.Infrastructure.DataContext
{
    public class EnrollmentDbSeeder : IDatabaseSeeder
    {
        private readonly EnrollmentDbContext _enrollmentDbContext;
        private readonly EnrollmentSupportDbContext _supportDbContext;

        public EnrollmentDbSeeder(EnrollmentDbContext enrollmentDbContext, EnrollmentSupportDbContext supportDbContext)
        {
            _enrollmentDbContext = enrollmentDbContext;
            _supportDbContext = supportDbContext;
        }

        private static async Task MigrateContextAsync(DbContext context, CancellationToken cancellationToken = default)
        {
            var migrations = context.Database.GetMigrations();
            if(!migrations.Any())
                return;

            var pending = await context.Database.GetPendingMigrationsAsync(cancellationToken);
            if (!pending.Any())
                return;

            await context.Database.MigrateAsync(cancellationToken);
        }

        public async Task SeedAsync(CancellationToken cancellationToken)
        {
            await MigrateContextAsync(_enrollmentDbContext, cancellationToken);
            await MigrateContextAsync(_supportDbContext, cancellationToken);
        }
    }
}
