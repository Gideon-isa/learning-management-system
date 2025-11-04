using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.CourseManagement.Application.Features.CourseFeatures.Commands.DeleteCourse
{
    public class DeleteCourseCommand : ICustomRequest<IResponseWrapper>
    {
        public Guid CourseId { get; set; }
    }
}
