using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.CourseManagement.Application.Features.Course.Queries.GetCourseById
{
    public class GetCourseByIdQuery : ICustomRequest<IResponseWrapper>
    {
        public Guid CourseId { get; set; }
    }
}
