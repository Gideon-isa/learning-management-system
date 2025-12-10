using Lms.CourseManagement.Application.Features.CourseCategory.DTO;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.CourseManagement.Application.Features.CourseCategory.Queries.GetAllCategories
{
    public class GetCourseCategoriesQuery : ICustomRequest<IResponseWrapper<List<CourseCategoryDto>>>
    {
    }
}
