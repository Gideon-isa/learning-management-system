using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.Enrollment.Application.Features.CourseEnrollment.Queries
{
    public class GetEnrollmentByIdQuery : ICustomRequest<IResponseWrapper<CourseEnrollmentResponse>>
    {
        public Guid Id { get; set; }

    }
}
