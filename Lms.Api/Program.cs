using Lms.Api.DependencyInjections;
using Lms.Api.Middleware;
using Lms.Assessment.Application.EventHandlers;
using Lms.CourseManagement.Application;
using Lms.CourseManagement.Application.DI;
using Lms.CourseManagement.Application.Features.CourseFeatures.Commands.PublishCourse;
using Lms.CourseManagement.Infrastructure.Configuration;
using Lms.CourseManagement.Infrastructure.DI;
using Lms.Enrollment.Application.DI;
using Lms.Enrollment.Application.EventHandlers;
using Lms.Identity.Application;
using Lms.Identity.Application.DependencyInjection;
using Lms.Identity.Infrastructure.DI;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.Shared.Application;
using Lms.Shared.Application.CustomMediator.Interfaces.Notification;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddExceptionHandler<ErrorHandlingMiddleware>();
builder.Services.AddProblemDetails();

// string to enum conversion
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddIdentityInfrastructureServices(builder.Configuration);
builder.Services.AddSharedApplicationServices(builder.Configuration);
builder.Services.AddApiApplicationServices(builder.Configuration);
builder.Services.AddIdentityApplicationServices();
builder.Services.AddCourseInfrastructureServices(builder.Configuration);
builder.Services.AddCourseManagementApplicationServices();
builder.Services.AddEnrollmentApplication();

//Register all request notification handlers from all application layers
builder.Services.Scan(scan => scan
    .FromAssemblies(
        typeof(ICourseManagementMarker).Assembly,
        typeof(CoursePublishedHandler).Assembly,
        typeof(EventHandlerMarker).Assembly,
        typeof(IIdentityApplicationMarker).Assembly
    )
    .AddClasses(classes => classes.AssignableTo(typeof(ICustomRequestHandler<,>)))
    .AsImplementedInterfaces()
    .WithScopedLifetime()
    .AddClasses(classes => classes.AssignableTo(typeof(ICustomNotificationHandler<>)))
    .AsImplementedInterfaces()
    .WithScopedLifetime());

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Use the handler early in the pipline
app.UseExceptionHandler();
try
{
    await app.AddDatabaseInintializerAsync();
}
catch (Exception ex)
{

    throw;
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseAuthorization();

app.UseOpenApiDocumentation();

app.MapControllers();

app.Run();
