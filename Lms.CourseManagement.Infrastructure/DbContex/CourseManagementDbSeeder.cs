
using Lms.Shared.Abstractions.DatabaseSeeder;
using Microsoft.EntityFrameworkCore;

namespace Lms.CourseManagement.Infrastructure.DbContex
{
    public class CourseManagementDbSeeder : IDatabaseSeeder
    {
        private readonly CourseManagementDbContext _courseManagementDbcontext;
        private readonly CourseSupportDbContext _courseSupportDbContextSupport;
        public CourseManagementDbSeeder(CourseManagementDbContext courseManagementDbcontext, CourseSupportDbContext courseSupportDbContextSupport)
        {
            _courseManagementDbcontext = courseManagementDbcontext;
            _courseSupportDbContextSupport = courseSupportDbContextSupport;
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
            await MigrateContextAsync(_courseManagementDbcontext, cancellationToken);
            await MigrateContextAsync(_courseSupportDbContextSupport, cancellationToken);
        }
    }
}
