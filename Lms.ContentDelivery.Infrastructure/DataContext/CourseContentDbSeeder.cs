using Lms.Shared.Abstractions.DatabaseSeeder;
using Microsoft.EntityFrameworkCore;

namespace Lms.ContentDelivery.Infrastructure.DataContext
{
    public class CourseContentDbSeeder : IDatabaseSeeder
    {
        private readonly CourseContentDbContext _courseContentDbcontext;
        private readonly CourseContentSupportDbContext _courseContentSupportDbContextSupport;
        public CourseContentDbSeeder(CourseContentDbContext courseContentDbcontext, CourseContentSupportDbContext courseContentSupportDbContextSupport)
        {
            _courseContentDbcontext = courseContentDbcontext;
            _courseContentSupportDbContextSupport = courseContentSupportDbContextSupport;
        }

        private static async Task MigrateContextAsync(DbContext context, CancellationToken cancellationToken = default)
        {
            var migrations = context.Database.GetMigrations();
            if (!migrations.Any())
                return;

            var pending = await context.Database.GetPendingMigrationsAsync(cancellationToken);
            if (!pending.Any())
                return;

            await context.Database.MigrateAsync(cancellationToken);
        }

        public async Task SeedAsync(CancellationToken cancellationToken)
        {
            await MigrateContextAsync(_courseContentDbcontext, cancellationToken);
            await MigrateContextAsync(_courseContentSupportDbContextSupport, cancellationToken);
        }
    }
}
