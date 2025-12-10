using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.CourseManagement.Application.Features.CourseCategory.Command.CreateCategory
{
    public class CreateCourseCategoryCommand : ICustomRequest<IResponseWrapper<Guid>>
    {
        public string Name { get; set; } = string.Empty;
    }
}
