using Lms.CourseManagement.Domain.Entities;

namespace Lms.CourseManagement.Application.Features.Course.DTO
{
    public class CourseResponse
    {
        public Guid Id { get; set; }
        public string CourseTitle { get; set; }
        public string CourseCode { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public CourseStatus Status { get; set; }
        public Guid InstructorId { get; set; }
        public DateTime? PublishedOn { get;  set; }
    }

    public class CoursesResponse
    {
        public List<CourseResponse>? courseResponses { get; set; }
    }
}
