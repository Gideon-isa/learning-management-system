using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.ContentDelivery.Application.Features.Queries.StudentModule
{
    public class GetMouduleByIdQuery : ICustomRequest<IResponseWrapper<List<StudentModuleResponse>>>
    {
        public Guid CourseId { get; set; }
        public Guid ModuleId { get; set; }
    }
}
