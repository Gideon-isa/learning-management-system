using System.Net;

namespace Lms.Identity.Application.Exceptions
{
    public class UnauthorizedException : Exception
    {

        public List<string> ErrorMessages { get; set; }
        public HttpStatusCode StatusCode { get; set; } 

        public UnauthorizedException(List<string> errorMessage = default, HttpStatusCode statusCode = HttpStatusCode.Unauthorized)
        {
            ErrorMessages = errorMessage;
            StatusCode = statusCode;
        }

    }
}
