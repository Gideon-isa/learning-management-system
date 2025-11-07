namespace Lms.Enrollment.Application.Features.CourseEnrollment
{
    public class CourseEnrollmentResponse
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string CourseTitle { get; set; } = string.Empty;


    }
}
