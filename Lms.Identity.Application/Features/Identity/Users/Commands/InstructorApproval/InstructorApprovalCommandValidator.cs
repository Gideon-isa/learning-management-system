using FluentValidation;
using Lms.SharedKernel.Domain.Enums;

namespace Lms.Identity.Application.Features.Identity.Users.Commands.InstructorApproval
{
    public class InstructorApprovalCommandValidator : AbstractValidator<InstructorApprovalCommand>
    {
        public InstructorApprovalCommandValidator()
        {
            RuleFor(u => u.ApprovalStatus)
                .IsInEnum()
                .Must(status => status != InstructorStatus.None && status != InstructorStatus.Requested)
                .WithMessage("ApprovalStatus cannot be None or Requested");
        }
    }
}
