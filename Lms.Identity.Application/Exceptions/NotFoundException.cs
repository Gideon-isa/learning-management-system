using System.Net;

namespace Lms.Identity.Application.Exceptions
{
    public class NotFoundException : Exception
    { 
        public List<string> ErrorMessages { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public NotFoundException(List<string> errorMessage = null, HttpStatusCode statusCode = HttpStatusCode.NotFound)
        {
            ErrorMessages = errorMessage ?? [];
            StatusCode = statusCode;
        }
    }
}
