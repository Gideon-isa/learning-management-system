using Lms.Shared.Abstractions.DatabaseSeeder;

namespace Lms.Api.DependencyInjections
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            
            return services
                .AddOpenApiDocumentation(configuration);      
        }

        public static async Task UseDatabaseSeedersAsync(this WebApplication app, IEnumerable<Type> requiredSeederTypes,CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(requiredSeederTypes);

            using var scope = app.Services.CreateScope();
            var provider = scope.ServiceProvider;

            var allSeeders = provider.GetServices<IDatabaseSeeder>().ToList();

            var missingSeeders = requiredSeederTypes.Where(t => allSeeders.All(s => s.GetType() != t)).ToList();

            if (missingSeeders.Count > 0)
                throw new InvalidOperationException($"The following required database seeders are missing: {string.Join(", ", missingSeeders.Select(t => t.Name))}");

            //
            foreach (var seedType in requiredSeederTypes)
            {
                var seeder = allSeeders.Single(s => s.GetType() == seedType);
                await seeder.SeedAsync(cancellationToken);
            }
        }


        /**
         * 
         *  NOT USING SCOPE
             * var seeder = app.Services.GetRequiredService<IDatabaseSeeder>();
             
            ❗ This resolves the service from the Root Service Provider.
            What does that mean?
            The object you get has no scope, so it behaves like a singleton.

            Any scoped dependencies inside the service become root-scoped singletons.

            EF Core DbContexts resolved this way become application-wide singletons → ❌ Very dangerous.

            ❗ Why this is bad?
                Because:

                Scoped services (e.g., DbContext) must not be used outside a scope.

                Resolving them from the root provider creates “captured” DbContexts that live longer than expected.

                This causes:

                race conditions

                invalid object state

                memory leaks

                EF Core tracking corruption

                “Cannot consume scoped service from singleton” exceptions

            When is Method 1 acceptable?
            Almost never EXCEPT for:

            Singleton services that depend only on singletons.

            Application-level services that have no scoped dependencies.

            Database seeders DO NOT qualify. 
        


            USING SCOPE
            using var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<IDatabaseSeeder>();

            ✔️ This resolves the service inside a scope, same as for an HTTP request.
            Why is this preferred?
            Because:

            1. Scoped services are correctly constructed
            IDatabaseSeeder often depends on:

            DbContext

            UnitOfWork

            Repositories

            EF Core transaction services

            All of these are scoped, so they MUST be created within a scope.
         */
    }
}
