namespace Lms.CourseManagement.Application.Features.Tags
{
    public class LessonTagResponse
    {
        public Guid Id { get; set; }
        public string TagName { get; set; } = string.Empty;
    }

    public class LessonTagResponses
    {
        public List<LessonTagResponse> LessonTags { get; set; }
    }
}
