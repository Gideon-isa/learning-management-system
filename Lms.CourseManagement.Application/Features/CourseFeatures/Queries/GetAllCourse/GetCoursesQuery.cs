using Lms.CourseManagement.Application.Features.Course.DTO;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.CourseManagement.Application.Features.Course.Queries.GetAllCourse
{
    public class GetCoursesQuery : ICustomRequest<IResponseWrapper<CoursesResponse>>
    {
    }
}
