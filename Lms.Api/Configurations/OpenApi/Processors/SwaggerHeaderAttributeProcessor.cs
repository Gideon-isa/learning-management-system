using Lms.Api.Configurations.OpenApi;
using NJsonSchema;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;
using System.Reflection;

namespace Lms.Api.Configurations.OpenApi.Processors
{
    public class SwaggerHeaderAttributeProcessor : IOperationProcessor
    {
        public bool Process(OperationProcessorContext context)
        {
            if (context.MethodInfo.GetCustomAttribute(typeof(SwaggerHeaderAttribute)) is SwaggerHeaderAttribute swaggerHeader)
            {
                var parameters = context.OperationDescription.Operation.Parameters;
                var existingParam = parameters.FirstOrDefault(p => p.Kind == NSwag.OpenApiParameterKind.Header && p.Name == swaggerHeader.HeaderName);

                if (existingParam is not null)
                {
                    parameters.Remove(existingParam);
                }

                parameters.Add(new NSwag.OpenApiParameter
                {
                    Name = swaggerHeader.HeaderName,
                    Kind = NSwag.OpenApiParameterKind.Header,
                    Description = swaggerHeader.Description,
                    IsRequired = true,
                    Schema = new JsonSchema
                    {
                        Type = JsonObjectType.String,
                        Default = swaggerHeader.DefaultValue
                    }
                });

               
                
            }
                return true;
        }
    }
}
