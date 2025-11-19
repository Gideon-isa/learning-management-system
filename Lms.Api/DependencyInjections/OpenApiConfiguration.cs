using Lms.Api.Configurations.OpenApi.Options;
using Lms.Api.Configurations.OpenApi.Processors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NSwag.Generation.Processors.Security;

namespace Lms.Api.DependencyInjections
{
    public static class OpenApiConfiguration
    {
        internal static IServiceCollection AddOpenApiDocumentation(this IServiceCollection services, IConfiguration config)
        {
            var swaggerSettings = config.GetSection(nameof(SwaggerSettings)).Get<SwaggerSettings>();
            services.AddEndpointsApiExplorer();
         
            services.AddOpenApiDocument((document, serviceProider) =>
            {
                
                document.PostProcess = doc =>
                {
                    doc.Info.Title = swaggerSettings?.Title;
                    doc.Info.Description = swaggerSettings?.Description;
                    doc.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = swaggerSettings?.ContactName,
                        Email = swaggerSettings?.ContactEmail,
                        Url = swaggerSettings?.ContactUrl,
                    };
                    doc.Info.License = new NSwag.OpenApiLicense
                    {
                        Name = swaggerSettings?.LicenseName,
                        Url = swaggerSettings?.LicenseUrl,
                    };
                    
                    
                };

                document.AddSecurity(JwtBearerDefaults.AuthenticationScheme, new NSwag.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter your Bearer token to attach it as a header on you request",
                    In = NSwag.OpenApiSecurityApiKeyLocation.Header,
                    Type = NSwag.OpenApiSecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT"
                });

              

                var xmlPath = Path.Combine(AppContext.BaseDirectory, "Lms.Api.xml");

                document.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor());
                document.OperationProcessors.Add(new SwaggerGlabalAuthProcessor());
                document.OperationProcessors.Add(new SwaggerHeaderAttributeProcessor());
                //document.OperationProcessors.Add(new SwaggerListAttrbuteProcessor());

            });
            return services;
        }

        public static IApplicationBuilder UseOpenApiDocumentation(this IApplicationBuilder app)
        {
            app.UseOpenApi();
            app.UseSwaggerUi(options =>
            {
                options.DefaultModelsExpandDepth = -1;
                options.DocExpansion = "none";
                options.TagsSorter = "alpha";
            });

            return app;
        }
    }
}
