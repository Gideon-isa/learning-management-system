using Lms.Enrollment.Application.Abstractions;
using Lms.Enrollment.Domain.Respositories;
using Lms.Enrollment.Infrastructure.DataContext;
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
            services.AddScoped<IAvailableCoursesRepository, AvailableCourseRepository>()
                    .AddScoped<ICourseEnrollmentRespository, CourseEnrollmentRepository>()
                    .AddScoped<IStudentRepository, StudentRepository>()
                    .AddScoped<IEnrollmentUnitOfWork, UnitOfWork>();
                    

            services.AddDbContext<EnrollmentDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
