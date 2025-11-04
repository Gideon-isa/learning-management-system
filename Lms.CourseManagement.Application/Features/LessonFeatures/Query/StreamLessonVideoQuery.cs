using Lms.CourseManagement.Application.Features.LessonFeatures.Dto;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.CourseManagement.Application.Features.LessonFeatures.Query
{
    public class StreamLessonVideoQuery : ICustomRequest<IResponseWrapper<StreamLessonVideoResponse>>
    {
        public Guid LessonId { get; set; }
        public string VideoTitle { get; set; } = string.Empty;

    }

}
