using FluentValidation;
using Lms.Shared.Application.Contracts;
using Lms.Shared.Application.EventDispatcher;
using Lms.Shared.Application.Sorting;
using Lms.Shared.Application.Validation.Pipeline;
using Lms.Shared.Application.Validations;
using Lms.SharedKernel.Application;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Lms.Shared.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSharedApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // Adding the Application config. values
            services.OptionsConfigure<AppOptions>(config);

            services.AddScoped<ICommandDispatcher, CommandDispatcher>();


            // Registers Fluent Validation
            var assembly = Assembly.GetExecutingAssembly();
            services.AddValidatorsFromAssembly(assembly)
                    .AddMediatR(cfg =>
                    {
                        cfg.RegisterServicesFromAssembly(assembly);
                    });
            // Registering the Validation Pipeline   
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
            //services.AddTransient<SortMappingProvider>();
            return services;
        }


        public static IServiceCollection OptionsConfigure<TOption>(
            this IServiceCollection services, IConfiguration configuration, string? sectionName = null) where TOption : class, new()
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(configuration);

            sectionName ??= typeof(TOption).Name;
            var option = new TOption();
            configuration.GetSection(typeof(TOption).Name).Bind(option);
            services.AddSingleton(option);

            return services;
        }

        //public static IServiceCollection AddCustomMediator(this IServiceCollection services, Assembly assembly)
        //{

        //    //  1. Register Request Handlers
        //    var requestHandlers = assembly.GetTypes()
        //        .Where(t => !t.IsAbstract && !t.IsInterface)
        //        .SelectMany(t => t.GetInterfaces()
        //            .Where(i => i.IsGenericType &&
        //                        i.GetGenericTypeDefinition() == typeof(ICustomRequestHandler<,>))
        //            .Select(i => new { Interface = i, Implementation = t }))
        //        .ToList();

        //    foreach (var handler in requestHandlers)
        //        services.AddScoped(handler.Interface, handler.Implementation);

        //    // 2. Register Notification Handlers
        //    var notificationHandlers = assembly.GetTypes()
        //        .Where(t => !t.IsAbstract && !t.IsInterface)
        //        .SelectMany(t => t.GetInterfaces()
        //            .Where(i => i.IsGenericType &&
        //                        i.GetGenericTypeDefinition() == typeof(ICustomNotificationHandler<>))
        //            .Select(i => new { Interface = i, Implementation = t }))
        //        .ToList();

        //    foreach (var handler in notificationHandlers)
        //        services.AddScoped(handler.Interface, handler.Implementation);

        //    // 3. Register the Mediator itself
        //    services.AddScoped<ICustomMediator, MiniMediator>();

        //    return services;
        //}
    }
}
