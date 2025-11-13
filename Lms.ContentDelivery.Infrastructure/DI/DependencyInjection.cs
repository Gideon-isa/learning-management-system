using Lms.ContentDelivery.Application.Abstractions;
using Lms.ContentDelivery.Domain.Repositories;
using Lms.ContentDelivery.Infrastructure.DataContext;
using Lms.ContentDelivery.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lms.ContentDelivery.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCourseContentInfrastructureServices(this IServiceCollection services, IConfiguration config) 
        {
            services.AddDbContext<CourseContentDbContext>(option => 
            {
                option.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IContentDeliveryUnitOfWork, ContentDeliveryUnitOfWork>();
            services.AddScoped<ICourseContentRepository, CourseContentRepository>();
            services.AddScoped<IStudentAccessRepository, StudentAccessRepository>();

            return services;
        }
    }
}
