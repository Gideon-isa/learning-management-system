using Lms.Identity.Application.Exceptions;
using Lms.SharedKernel.Common.Wrappers;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Lms.Api.Middleware
{
    public class ErrorHandlingMiddleware : IExceptionHandler
    {
        
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (httpContext.Response.HasStarted)
            {
                return false;
            }


            var (statusCode, errorMessage) = exception switch
            {
                ConflictException ce => ((int)ce.StatusCode,  ce.ErrorMessages),
                NotFoundException nfe => ((int)nfe.StatusCode, nfe.ErrorMessages),
                ForbiddenException fe => ((int)fe.StatusCode, fe.ErrorMessages ),
                IdentityException ie => ((int)ie.StatusCode, ie.ErrorMessages ),
                UnauthorizedException ue => ((int)ue.StatusCode, ue.ErrorMessages ),
                _ => ((int)HttpStatusCode.InternalServerError, new List<string>{ "something went wrong. Contact admin" })
            };

            var responseWrapper = ResponseWrapper.Fail();
            responseWrapper.Messages = errorMessage;
          
            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = GetProblemTitle(statusCode),
                Detail = errorMessage?.FirstOrDefault(),
                Instance = httpContext.Request.Path
            };

            // Add your warapper to extensions (custom fields allowed by spec)
            problemDetails.Extensions["response"] = responseWrapper;

            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }

        private static string GetProblemTitle(int statusCode)
        {
            return statusCode switch
            {
                (int)HttpStatusCode.BadRequest => "Bad Request",
                (int)HttpStatusCode.Unauthorized => "Unauthorized",
                (int)HttpStatusCode.Forbidden => "Forbidden",
                (int)HttpStatusCode.NotFound => "Not Found",
                (int)HttpStatusCode.Conflict => "Conflict",
                (int)HttpStatusCode.InternalServerError => "Internal Server Error",
                _ => "Error"
            };
        }
    }
}
