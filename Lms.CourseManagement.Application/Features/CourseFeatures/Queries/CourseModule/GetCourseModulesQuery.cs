using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.CourseManagement.Application.Features.CourseFeatures.Queries.CourseModule
{
    public class GetCourseModulesQuery : ICustomRequest<IResponseWrapper<CourseModuleDto>>
    {
        public Guid CourseId { get; set; }
    }
}
