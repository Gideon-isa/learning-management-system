using Lms.CourseManagement.Application.Features.CourseCategory.DTO;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.CourseManagement.Application.Features.CourseCategory.Queries.GetCategory
{
    public class GetCourseCategoryQuery : ICustomRequest<IResponseWrapper<CourseCategoryDto>>
    {
        public Guid Id { get; set; }
    }
}
    