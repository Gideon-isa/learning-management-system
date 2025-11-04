using Lms.CourseManagement.Application.Features.Module.Dto;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.CourseManagement.Application.Features.Module.Queries
{
    public class GetCourseModuleByIdQuery : ICustomRequest<IResponseWrapper<CourseModuleResponse>>
    {
        public Guid ModuleId { get; set; }
        public Guid CourseId { get; set; }
    }
}
