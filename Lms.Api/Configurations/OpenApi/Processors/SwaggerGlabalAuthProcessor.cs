using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using NSwag;
using NSwag.Generation.AspNetCore;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;
using System.Collections.Concurrent;
using System.Reflection;

namespace Lms.Api.Configurations.OpenApi.Processors
{
    public class SwaggerGlabalAuthProcessor(string scheme) : IOperationProcessor
    {
        private readonly string _scheme = scheme;

        /// <summary>
        /// Initializes a new instance of the <see cref="SwaggerGlabalAuthProcessor"/> class with the default authentication scheme set to JWT Bearer.<see cref="JwtBearerDefaults.AuthenticationScheme"/>
        /// </summary>
        public SwaggerGlabalAuthProcessor() : this(JwtBearerDefaults.AuthenticationScheme)
        {
            
        }

        /// <summary>
        /// Process the given operation to conditionally apply security requirements for swagger documentation.
        /// </summary>
        /// <param name="context">The context containing operation and API metadat.</param>
        /// <returns>
        /// 
        /// </returns>
        /// <remarks>
        /// The processor checks for the presence of the <see cref="AllowAnonymousAttribute"/> in the endpoint's metadata. If present, it skips adding security requirements. If not present and no security requirements exist, it adds a security requirement for the specified authentication scheme.
        /// </remarks>
        public bool Process(OperationProcessorContext context)
        {
            var aspNetCoreContent = context as AspNetCoreOperationProcessorContext;
            IList<object>? list = ((AspNetCoreOperationProcessorContext)context).ApiDescription.ActionDescriptor.TryGetPropertyValue<IList<object>>("EndpointMetadata");

            // If metadate is missing, just skip
            if (list == null)
                return true;

            // skip endpoints with [AllowAnonymous]
            if (list.OfType<AllowAnonymousAttribute>().Any())
                return true;

            // Make sure the security list exists
            var operation = context.OperationDescription?.Operation;
            if (operation == null)
                return true;

            operation.Security ??= new List<OpenApiSecurityRequirement>();

            // Add global security requirement if not already present
            if (operation.Security.Count == 0 && _scheme != null)
            {
                operation.Security.Add(new OpenApiSecurityRequirement
                {
                    { _scheme, Array.Empty<string>() }

                });
            }
            return true;
        }

    }
    public static class ObjectExtension
    {
        /// <summary>
        /// Attempts to retrieve the value of a specified property from an object.
        /// If the property does not exist or is inaccessible, it returns a default value.
        /// </summary>
        /// <typeparam name="T">The expected type of the property value.</typeparam>
        /// <param name="obj">The object from which to retrieve the property value</param>
        /// <param name="propertyName">The name of the property to retrieve</param>
        /// <param name="defaultValue">
        /// The value of the property if it does not exist or is inaccessible.
        /// Defaults to the default value of type if not provided<typeparamref name="T"/>
        /// </param>
        /// <returns>
        /// The value of the specified property if it exists and is accessible; otherwise, the provided default value or the default value of type <typeparamref name="defaultValue"/>.
        /// </returns>
        /// <remarks>
        /// This method uses reflection to inspect the object's type and retrieve the property value dynamically. 
        /// It is useful in scenarios where the property name is not known at compile time or when working with dynamic objects.
        /// </remarks>

        // Optional cache to avoid repeated reflection lookups (which are slow)
        private static readonly ConcurrentDictionary<(Type, string), PropertyInfo?> _propertyCache = new();
        public static T? TryGetPropertyValue<T>(this object obj, string propertyName, T? defaultValue = default)
        {
            if (obj is null || string.IsNullOrEmpty(propertyName))
                return defaultValue;

            var type = obj.GetType();

            // Look up the property info in the cache or fetch via reflection if not cached
            var propertyInfo = _propertyCache.GetOrAdd((type, propertyName), key =>
            {
                var (t, propName) = key;
                return t.GetRuntimeProperty(propName);
            });

            if (propertyInfo == null)
                return defaultValue;

            // Safely get the property value and cast to the expected type
            return propertyInfo.GetValue(obj) is T value ? value : defaultValue;
        }




        /**
         * return obj
                .GetType()
                .GetRuntimeProperty(propertyName) is PropertyInfo propertyInfo 
                ? (T)propertyInfo.GetValue(obj) : defaultValue;
         * 
         * 
         */

    }
}
