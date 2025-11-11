using Lms.ContentDelivery.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lms.ContentDelivery.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCourseContentInfrastructureServices(this IServiceCollection services, IConfiguration config) 
        {
            return services.AddDbContext<CourseContentDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            });
        }
    }
}
