using Lms.Identity.Application.Abstractions;
using Lms.Identity.Application.Events;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.Shared.Application.Contracts;
using Lms.SharedKernel.Application;
using Lms.SharedKernel.Common.Wrappers;
using Lms.SharedKernel.Domain.Enums;
using Mapster;

namespace Lms.Identity.Application.Features.Identity.Users.Commands.InstructorApproval
{
    /// <summary>
    /// 
    /// </summary>
    public class InstructorApprovalCommandHandler : ICustomRequestHandler<InstructorApprovalCommand, IResponseWrapper>
    {
        private readonly IUserService _userService;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly IIdentityUnitOfWork _identityUnitOfWork; 

        public InstructorApprovalCommandHandler(IUserService userService, IDomainEventDispatcher domainEventDispatcher, IIdentityUnitOfWork identityUnitOfWork)
        {
            _userService = userService;
            _domainEventDispatcher = domainEventDispatcher;
            _identityUnitOfWork = identityUnitOfWork;
        }
         
        public async Task<IResponseWrapper> Handle(InstructorApprovalCommand request, CancellationToken cancellationToken)
        {
            var (isSuccessful, userResponse)  = await _userService.ApproveInstructorRequestAsync(request, cancellationToken);
            var instructorEvent = new InstructorRequest();
            var responseMessage = "User request rejected"; 

            if (isSuccessful && request.ApprovalStatus == InstructorStatus.Approved)
            { 
                var approvedInstructorEvent = userResponse.Adapt<ApprovedInstructorEvent>();

                instructorEvent.AddDomainEvent(approvedInstructorEvent);

                await _domainEventDispatcher.DispatchAsync(instructorEvent.DomainEvents, cancellationToken);

                instructorEvent.ClearDomainEvents();
                responseMessage = "User request to be assigned Instructor has been approved successfully";
            }
            
            await _identityUnitOfWork.SaveChangesAsync(cancellationToken);
            return await ResponseWrapper.SuccessAsync([responseMessage]);
        }
    }
}
