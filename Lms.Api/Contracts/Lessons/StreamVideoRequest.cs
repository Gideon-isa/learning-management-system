namespace Lms.Api.Contracts.Lessons
{
    public class StreamVideoRequest
    {
        public Guid LessonId { get; set; }
        public string VideoTitle { get; set; } = string.Empty;
    }
}
