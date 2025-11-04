using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.CourseManagement.Application.Features.Tags.Command
{
    public class CreateLessonTagCommand : ICustomRequest<IResponseWrapper<LessonTagResponse>>
    {
        public string TagName { get; set; }
    }
}
