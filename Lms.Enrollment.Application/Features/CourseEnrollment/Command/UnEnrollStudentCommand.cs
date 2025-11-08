using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.Enrollment.Application.Features.CourseEnrollment.Command
{
    public class UnEnrollStudentCommand : ICustomRequest<IResponseWrapper>
    {
        public Guid EnrollmentId { get; set; }
        public Guid StudentId { get; set; }
    }
}
