using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.Enrollment.Application.Features.CourseEnrollment.Command
{
    public class CreateEnrollmentCommand : ICustomRequest<IResponseWrapper<CourseEnrollmentResponse>>
    {
        public Guid CourseId { get; set; }
        public List<Guid> StudentIds { get; set; }
    }
}
