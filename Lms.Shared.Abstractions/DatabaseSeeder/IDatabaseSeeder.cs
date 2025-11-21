namespace Lms.Shared.Abstractions.DatabaseSeeder
{
    public interface IDatabaseSeeder
    {
        Task SeedAsync(CancellationToken cancellationToken);
    }
}
