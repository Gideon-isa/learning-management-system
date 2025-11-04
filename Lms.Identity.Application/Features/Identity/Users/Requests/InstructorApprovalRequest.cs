using Lms.SharedKernel.Domain.Enums;

namespace Lms.Identity.Application.Features.Identity.Users.Requests
{
    public class InstructorApprovalRequest
    {
        public InstructorStatus IsApproved { get; set; }
    }
}
