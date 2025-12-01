using Lms.SharedKernel.Domain;

namespace Lms.CourseManagement.Domain.Exceptions
{
    public class PublishedCourseException : DomainException
    {
        public PublishedCourseException() { }

        public PublishedCourseException(string message) : base(message) { }

        public PublishedCourseException(string message, Exception innerException) : base(message, innerException) { }
    }
}
