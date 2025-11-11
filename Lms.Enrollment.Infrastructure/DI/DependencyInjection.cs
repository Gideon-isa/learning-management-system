using Lms.Enrollment.Application.Abstractions;
using Lms.Enrollment.Domain.Respositories;
using Lms.Enrollment.Infrastructure.DataContext;
using Lms.Enrollment.Infrastructure.Outbox;
using Lms.Enrollment.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lms.Enrollment.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEnrollmentInfrastructureServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<EnrollmentDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IAvailableCoursesRepository, AvailableCourseRepository>()
                    .AddScoped<ICourseEnrollmentRespository, CourseEnrollmentRepository>()
                    .AddScoped<IStudentRepository, StudentRepository>()
                    .AddScoped<IEnrollmentUnitOfWork, UnitOfWork>()
                    .AddScoped<IEnrollmentIntegrationEventPublisher, OutboxService>()
                    .AddHostedService<OutboxProcessor>();

            return services;
        }
    }
}
