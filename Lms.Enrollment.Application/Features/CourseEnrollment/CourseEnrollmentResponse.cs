using Lms.Enrollment.Domain.Entities;
using Lms.Enrollment.Domain.Enums;

namespace Lms.Enrollment.Application.Features.CourseEnrollment
{
    public class CourseEnrollmentResponse
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string CourseTitle { get; set; } = string.Empty;
        public List<StudentEnrollmentResponse> StudentEnrollments { get; set; } = [];
    }

    public class StudentEnrollmentResponse 
    {
        public Guid StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public DateTime EnrolledOn { get; set; }
        public EnrollmentStatus Status { get; set; }

    }


}
