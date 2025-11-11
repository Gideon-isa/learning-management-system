using Lms.CourseDelivery.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lms.CourseDelivery.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCourseDeliveryInfrastructureServices(this IServiceCollection services, IConfiguration config) 
        {
            return services.AddDbContext<CourseDeliveryDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            });
        }
    }
}
