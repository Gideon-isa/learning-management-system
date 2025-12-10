using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Lms.Api.Configurations
{
    public class ProducesResponseTypeConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            application.Controllers.ToList().ForEach(controller =>
            {
                controller.Actions.ToList().ForEach(action =>
                {
                    var httpMethods = action.Attributes
                                    .OfType<HttpMethodAttribute>()
                                    .SelectMany(a => a.HttpMethods)
                                    .Select(m => m.ToUpperInvariant())
                                    .Distinct();

                    foreach (var httpMethod in httpMethods)
                    {
                        switch (httpMethod)
                        {
                            case "GET":
                                action.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status200OK));
                                action.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status404NotFound));
                                break;

                            case "POST":
                                action.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status201Created));
                                action.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
                                action.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status409Conflict));
                                break;

                            case "PUT":
                                action.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status200OK));
                                action.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
                                break;

                            case "DELETE":
                                action.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status204NoContent));
                                action.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status404NotFound));
                                break;
                        }

                    }

                    // Common to all actions
                    AddIfNotExists(action, StatusCodes.Status401Unauthorized);
                    AddIfNotExists(action, StatusCodes.Status500InternalServerError);
                    AddIfNotExists(action, StatusCodes.Status429TooManyRequests);

                    if (!action.Filters.OfType<ProducesDefaultResponseTypeAttribute>().Any())
                    {
                        action.Filters.Add(new ProducesDefaultResponseTypeAttribute(typeof(ProblemDetails)));
                    }
                });
            });
        }

        private static void AddIfNotExists(ActionModel action, int statusCode)
        {
            if (!action.Filters.OfType<ProducesResponseTypeAttribute>().Any(s => s.StatusCode == statusCode))
            {
                action.Filters.Add(new ProducesResponseTypeAttribute(statusCode));
            }
        }
    }
}
