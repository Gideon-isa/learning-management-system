using Lms.CourseManagement.Application.Features.CourseFeatures.Commands.CreateCourse;

namespace Lms.CourseManagement.Application.Features.LessonFeatures.Dto
{
    public class LessonResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<LessonImageResponse> Videos { get; set; }
        public List<LessonImageResponse> Images { get; set; }

    }

    public class LessonVideoResponse
    {
        //public int Id { get; set; }
        public string Path { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class LessonImageResponse
    {
        //public int Id { get; set; }
        public string Path { get; set; } = string.Empty;
        public string Caption { get; set; } = string.Empty;
    }


}
