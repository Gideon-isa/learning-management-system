using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.Enrollment.Application.Features.CourseEnrollment.Queries
{
    public class GetAllAvailableStudentQuery : ICustomRequest<IResponseWrapper<List<AvailableStudentResponse>>>
    {
    }
}
