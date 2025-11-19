using Lms.CourseManagement.Application.Features.Course.DTO;
using Lms.CourseManagement.Domain.Entities;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.CourseManagement.Application.Features.Course.Queries.GetAllCourse
{
    public class GetCoursesQuery : ICustomRequest<IResponseWrapper<CoursesResponse>>
    {
        public string? Search { get; set; }
        public string CourseTitle { get; set; }
        public string CourseCode { get; set; }
        public string Description { get; set; }
        public string Category { get;  set; }
        public string? Sort { get; init; }
        public string? Fields { get; init; }
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
}
