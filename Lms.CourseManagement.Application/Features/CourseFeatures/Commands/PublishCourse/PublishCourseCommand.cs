using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.CourseManagement.Application.Features.Course.Commands.PublishCourse
{
    public class PublishCourseCommand : ICustomRequest<IResponseWrapper>
    {
        public Guid CourseId { get; set; }
    }
}
