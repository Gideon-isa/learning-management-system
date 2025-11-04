using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.CourseManagement.Application.Features.Lesson.Command
{
    public class CreateLessonCommand : ICustomRequest<IResponseWrapper>
    {
        public Guid CourseId { get; set; }
        public Guid ModuleId { get;  set; }
        public string Title { get; set; } = string.Empty;
        public int Order { get; set; }
        public TimeSpan Duration { get; set; }
        public string Description { get; set; } = string.Empty;
        public VideoFileDto? Video { get; set; } = new();
        public List<ImageFileDto>? Images { get; set; } = new();
        public List<Guid>? TagIds { get; set; } = new();
    }

    public class VideoFileDto
    {
        public Stream Content { get; set; } = default!;
        public string FileName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string ThumbNail { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class ImageFileDto
    {
        public Stream Content { get; set; } = default!;
        public string FileName { get; set; } = string.Empty;
        public string Caption { get; set; } = string.Empty;
    }
}
