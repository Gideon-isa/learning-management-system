using Lms.CourseManagement.Application.Abstractions;
using Lms.CourseManagement.Domain.Repositories;
using Lms.CourseManagement.Infrastructure.DbContex;
using Lms.CourseManagement.Infrastructure.Outbox;
using Lms.CourseManagement.Infrastructure.Persistence;
using Lms.Shared.IntegrationEvents.Integration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lms.CourseManagement.Infrastructure.DI
{
    public static class DependencyInjection 
    {
        public static IServiceCollection AddCourseInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CourseManagementDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseManagementUnitOfWork, UnitofWork>();
            services.AddHostedService<OutboxProcessor>();
            services.AddScoped<ICourseIntegrationEventPublisher, OutboxService>();
            services.AddScoped<ILessonRespositoy, LessonRepository>();
            services.AddScoped<ILessonTagRespository, LessonTagRepository>();
            services.AddScoped<MediatRIntegrationEventPublisher>(); // implementing the concrete
            services.AddScoped<IInstructorRepository, InstructorRepository>();
           
            return services;
        }
    }
}
