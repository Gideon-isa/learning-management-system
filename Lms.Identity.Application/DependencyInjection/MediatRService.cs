using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Lms.Identity.Application.DependencyInjection
{
    public static class MediatRService
    {
        public static IServiceCollection AddMediatetorService(this IServiceCollection services) 
        {
            return services;
        }
    }
}
