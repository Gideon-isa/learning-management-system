using Lms.Identity.Application.Abstractions;
using Lms.Identity.Application.Events;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Application;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.Identity.Application.Features.Identity.Users.Commands.AddInstructor
{
    public class AddInstructorCommandHandler : ICustomRequestHandler<AddInstructorCommand, IResponseWrapper>
    {
        private readonly IUserService _userService;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly IIdentityUnitOfWork _identityUnitOfWork;

        public AddInstructorCommandHandler(IUserService userService, IDomainEventDispatcher domainEventDispatcher, IIdentityUnitOfWork identityUnitOfWork)
        {
            _userService = userService;
            _domainEventDispatcher = domainEventDispatcher;
            _identityUnitOfWork = identityUnitOfWork;
        }
        public async Task<IResponseWrapper> Handle(AddInstructorCommand request, CancellationToken cancellationToken)
        {
            var userDto = await _userService.InitiateInstructorRoleAsync(request.UserIdOrEmail, cancellationToken);
            var instructorReqest = new InstructorRequest();

            instructorReqest.AddDomainEvent(new InstructorRoleRequestedEvent(userDto.Email, userDto.FirstName, userDto.LastName));
            await _domainEventDispatcher.DispatchAsync(instructorReqest.DomainEvents, cancellationToken);

            instructorReqest.ClearDomainEvents();
            await _identityUnitOfWork.SaveChangesAsync();

            return await ResponseWrapper.SuccessAsync("Success");
        }
    }
}
