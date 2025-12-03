using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.CourseManagement.Application.Features.Module.Commands
{
    public class DeleteCourseModuleCommand : ICustomRequest<IResponseWrapper>
    {
        public Guid ModuleId { get; set; }
        public Guid CourseId { get; set; }
    }

}
