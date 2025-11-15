namespace Lms.ContentDelivery.Application.Features.Queries.StudentModule
{
    public class StudentAccessCourseResponse
    {
        public string StudentCode { get;  set; } = string.Empty;
        public List<CourseContentDto> Courses { get; set; } = [];
    }

    public class CourseContentDto
    {
        public Guid CourseId { get; set; }
        public string CourseTitle { get; set; } = string.Empty;
        public string CourseCode { get; set; } = string.Empty;
        public List<CourseModuleContentDto> Modules { get; set; } = [];
    }

    public class CourseModuleContentDto
    {
        public Guid CourseModuleId { get; set; }
        public string ModuleTitle {  get; set; } = string.Empty;
        public List<LessonDto> Lessons { get; set; } = [];
    }

    public class LessonDto
    {
        public string Title { get; set; } = string.Empty;
        public TimeSpan Duration {  get; set; }
        public List<VideoDto> Videos { get; set; } = [];
    }

    public class VideoDto
    {
        public Guid VideoId { get; set; }
        public string Path { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Thumbnail {  get; set; } = string.Empty;
    }

}
