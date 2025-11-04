//using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using NJsonSchema;
//using NSwag.Generation.Processors;
//using NSwag.Generation.Processors.Contexts;

//namespace Lms.Api.Configurations.OpenApi.Processors
//{
//    public class SwaggerListAttrbuteProcessor : IOperationProcessor
//    {
//        public bool Process(OperationProcessorContext context)
//        {
//            // Check if the request body corresponds to the CreateLessonRequest type
//            if (context.MethodInfo.GetParameters()
//                .Any(p => p.ParameterType.GetProperties()
//                .Any(prop => prop.PropertyType == typeof(List<Guid>) || prop.PropertyType == typeof(IEnumerable<Guid>) || prop.PropertyType == typeof(IFormFile))))
//            {
//                // Loop through all media types 
//                foreach (var mediaType in context.OperationDescription.Operation.RequestBody?.Content ?? new Dictionary<string, NSwag.OpenApiMediaType>())
//                {
//                    var schema = mediaType.Value.Schema;
//                    if(schema?.Properties?.ContainsKey("TagIds") == true)
//                    {
//                        schema.Properties["TagIds"] = new JsonSchemaProperty
//                        {
//                            Type = JsonObjectType.Array,
//                            Item = new JsonSchema
//                            {
//                                Type = JsonObjectType.String,
//                                Format = "uuid"
//                            },

//                        };
//                    }

//                    if (schema?.Properties?.ContainsKey("ImageCaptions") == true)
//                    {
//                        schema.Properties["ImageCaptiosn"] = new JsonSchemaProperty
//                        {
//                            Type = JsonObjectType.Array,
//                            Item = new JsonSchema
//                            {
//                                Type = JsonObjectType.String,
//                            }
//                        };
//                    }

//                    if (schema?.Properties.ContainsKey("Images") == true)
//                    {
//                        schema.Properties["Images"] = new JsonSchemaProperty
//                        {
//                            Type = JsonObjectType.Array,
//                            Item = new JsonSchema
//                            {
//                                Type = JsonObjectType.String,
//                                Format = "binary"
//                            },
//                            Description = "Upload multiple image files"
//                        };
//                    }
//                }
//            }
  
//            return true;
//        }
//    }
//}
