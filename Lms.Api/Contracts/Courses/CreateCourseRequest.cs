namespace Lms.Api.Contracts.Courses
{
    public class CreateCourseRequest
    {
        public required string CourseTitle { get;  set; }
        public required string CourseCode { get;  set; }
        public required string Description { get; set; }
        public required string Category { get; set; }
        public required Guid InstructorId { get; set; }
        public DateTime? PublishedOn { get;  set; }
    }
}
