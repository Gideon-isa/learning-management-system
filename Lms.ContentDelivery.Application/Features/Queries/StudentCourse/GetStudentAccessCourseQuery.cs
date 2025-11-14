using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.ContentDelivery.Application.Features.Queries.StudentCourse
{
    public class GetStudentAccessCourseQuery : ICustomRequest<IResponseWrapper<StudentAccessCourseResponse>>
    {
        public string StudentCode { get; set; } = string.Empty;
    }
}
