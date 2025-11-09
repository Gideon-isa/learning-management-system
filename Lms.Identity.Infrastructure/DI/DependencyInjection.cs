using Lms.Identity.Application.Abstractions;
using Lms.Identity.Application.Features.Identity.Users;
using Lms.Identity.Infrastructure.Context;
using Lms.Identity.Infrastructure.Mapping;
using Lms.Identity.Infrastructure.Persistence;
using Lms.Identity.Infrastructure.Services;
using Lms.Identity.Infrastructure.Services.Outbox;
using Lms.Shared.IntegrationEvents.Integration;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lms.Identity.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIdentityInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {

            // Registering Mapster config
            MappingConfig.RegisterConfig();

            return services.AddDbContext<UserIdentityDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            })
            .AddTransient<ApplicationDbSeeder>()
            .AddPermissions()
            .AddDatabaseSeeder()
            .AddIdentityService()
            .AddOptions(config)
            .AddJwtAuthentication(config)
            .AddHostedService<IdentityOutboxProcessor>()
            .AddScoped<MediatRIntegrationEventPublisher>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IIdentityUnitOfWork, IdentityUnitOfWork>()
            .AddScoped<IIdentityIntegrationEventPublisher, IdentityOutboxService>()
            .AddScoped<IStudentCodeGenerator, StudentCodeGenerator>();

            //.AddCustomMediator(typeof(DependencyInjection).Assembly);


        }

        //internal static IServiceCollection AddPermissions(this IServiceCollection services)
        //{
        //    return services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
        //                   .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();          
        //}

        //public static IServiceCollection AddDatabaseSeeder(this IServiceCollection services)
        //{
        //    services.AddScoped<ApplicationDbSeeder>();
        //    return services;
        //}

        //private static IServiceCollection AddIdentityService(this IServiceCollection services)
        //{
        //    return services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        //    {
        //        options.Password.RequireDigit = false;
        //        options.Password.RequireLowercase = false;
        //        options.Password.RequireUppercase = false;
        //        options.Password.RequireNonAlphanumeric = false;
        //        options.Password.RequiredLength = 8;
        //        options.User.RequireUniqueEmail = true;

        //        options.User.RequireUniqueEmail = true;
        //    })
        //    .AddEntityFrameworkStores<ApplicationDbContext>()
        //    .AddDefaultTokenProviders()
        //    .Services
        //    .AddTransient<ITokenService, TokenService>()
        //    .AddScoped<IUnitOfWork, UnitOfWork>();
        //}

        public static async Task AddDatabaseInintializerAsync(this WebApplication app, CancellationToken cancellation = default)
        {
            using var scope = app.Services.CreateScope();
            var dbSeeder = scope.ServiceProvider.GetRequiredService<ApplicationDbSeeder>();
            await dbSeeder.InitializeDatabaseAsync(cancellation);
        }

        //private static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration config)
        //{  
        //    return services.OptionsConfigure<JwtOptions>(config);
   
        //}

        //private static JwtOptions? GetJwtOptions(IConfiguration config) => config.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

        //private static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
        //{
        //    var jwtOptions = GetJwtOptions(config) ?? null;
        //    if (jwtOptions == null)
        //        throw new InvalidOperationException("Jwt Options not configured");

        //    var secret = Encoding.ASCII.GetBytes(jwtOptions.Secret);

        //    services.AddAuthentication(auth =>
        //    {
        //        auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //        auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        //    })
        //    .AddJwtBearer(bearer =>
        //    {
        //        bearer.RequireHttpsMetadata = false;
        //        bearer.SaveToken = true;
        //        bearer.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            ValidateIssuer = false,
        //            ValidateAudience = false,
        //            ClockSkew = TimeSpan.Zero,
        //            RoleClaimType = ClaimTypes.Role,
        //            ValidateLifetime = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret))
        //        };
        //        // on failure of authentication, This Event will be raised
        //        bearer.Events = new JwtBearerEvents
        //        {
        //            OnAuthenticationFailed = context =>
        //            {
        //                if (context.Exception is SecurityTokenExpiredException)
        //                {
        //                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        //                    context.Response.ContentType = "application/json";
        //                    var result = JsonConvert.SerializeObject(ResponseWrapper.Fail("Token has expired."));
        //                    return context.Response.WriteAsync(result);
        //                }
        //                else
        //                {
        //                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        //                    context.Response.ContentType = "application/json";
        //                    var result = JsonConvert.SerializeObject(ResponseWrapper.Fail("An unhandled error has occured."));
        //                    return context.Response.WriteAsync(result);
        //                }
        //            },
        //            OnChallenge = context =>
        //            {
        //                context.HandleResponse();
        //                if (!context.Response.HasStarted)
        //                {
        //                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        //                    context.Response.ContentType = "application/json";
        //                    var result = JsonConvert.SerializeObject(ResponseWrapper.Fail("You are not authorized"));
        //                    return context.Response.WriteAsync(result);
        //                }
        //                return Task.CompletedTask;
        //            },
        //            OnForbidden = context =>
        //            {
        //                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
        //                context.Response.ContentType = "application/json";

        //                var result = JsonConvert.SerializeObject(ResponseWrapper.Fail("You are not authorized to access this resource."));
        //                return context.Response.WriteAsync(result);
        //            }
        //        };
        //    });

        //    services.AddAuthorization(options =>
        //    {
        //        foreach (var permission in Permissions.All)
        //        {
        //            options.AddPolicy(permission.Name, policy => policy.RequireClaim(ClaimConstants.Permission, permission.Name));
        //        }
        //    });
        //    return services;
        //}

        //private static IServiceCollection AddOpenApiDocumentation(this IServiceCollection services, IConfiguration config)
        //{
        //    var swaggerSettings = config.GetSection(nameof(SwaggerSettings)).Get<SwaggerSettings>();
        //    services.AddEndpointsApiExplorer();
        //    services.AddOpenApiDocument((document, serviceProider) =>
        //    {
        //        document.PostProcess = doc =>
        //        {
        //            doc.Info.Title = swaggerSettings?.Title;
        //            doc.Info.Description = swaggerSettings?.Description;
        //            doc.Info.Contact = new NSwag.OpenApiContact
        //            {
        //                Name = swaggerSettings?.ContactName,
        //                Email = swaggerSettings?.ContactEmail,
        //                Url = swaggerSettings?.ContactUrl,
        //            };
        //            doc.Info.License = new NSwag.OpenApiLicense
        //            {
        //                Name = swaggerSettings?.LicenseName,
        //                Url = swaggerSettings?.LicenseUrl,
        //            };
        //        };

        //        document.AddSecurity(JwtBearerDefaults.AuthenticationScheme, new NSwag.OpenApiSecurityScheme
        //        {
        //            Name = "Authorization",
        //            Description = "Enter your Bearer token to attach it as a header on you request",
        //            In = NSwag.OpenApiSecurityApiKeyLocation.Header,
        //            Type = NSwag.OpenApiSecuritySchemeType.Http,
        //            Scheme = JwtBearerDefaults.AuthenticationScheme,
        //            BearerFormat = "JWT"
        //        });

        //        document.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor());
        //        document.OperationProcessors.Add(new SwaggerGlabalAuthProcessor());
        //        document.OperationProcessors.Add(new SwaggerHeaderAttributeProcessor());

        //    });
        //    return services;
        //}

        //public static IApplicationBuilder UseOpenApiDocumentation(this IApplicationBuilder app)
        //{
        //    app.UseOpenApi();
        //    app.UseSwaggerUi(options =>
        //    {
        //        options.DefaultModelsExpandDepth = -1;
        //        options.DocExpansion = "none";
        //        options.TagsSorter = "alpha";
        //    });

        //    return app;
        //}

        //public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        //{
        //    return app.UseOpenApiDocumentation();
        //}
    }

}
