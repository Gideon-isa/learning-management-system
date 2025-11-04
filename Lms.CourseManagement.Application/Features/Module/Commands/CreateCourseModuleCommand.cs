using Lms.CourseManagement.Application.Features.Module.Dto;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.CourseManagement.Application.Features.Module.Commands
{
    public class CreateCourseModuleCommand : ICustomRequest<IResponseWrapper<CourseModuleResponse>>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid CourseId { get; set; }
    }
}
