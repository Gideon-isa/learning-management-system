using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using Lms.SharedKernel.Domain.Enums;

namespace Lms.Identity.Application.Features.Identity.Users.Commands.InstructorApproval
{
    public class InstructorApprovalCommand : ICustomRequest<IResponseWrapper>
    {
        public string UserIdOrEmail {  get; set; } = string.Empty;
        public InstructorStatus ApprovalStatus { get; set; }
    }
}
