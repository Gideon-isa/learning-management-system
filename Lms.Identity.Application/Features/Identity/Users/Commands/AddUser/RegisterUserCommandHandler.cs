using Lms.Identity.Application.Abstractions;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using Lms.SharedKernel.Interfaces;
using MediatR;

namespace Lms.Identity.Application.Features.Identity.Users.Commands.AddUser
{
    public class RegisterUserCommandHandler : ICustomRequestHandler<RegisterUserCommand, IResponseWrapper>
    {
        private readonly IUserService _userService;
        private readonly IIdentityUnitOfWork _unitOfWork;

        public RegisterUserCommandHandler(IUserService userService, IIdentityUnitOfWork unitOfWork)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResponseWrapper> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            // Step 1: Create identity user
            var identityUserId = await _userService.CreateAsync(command, cancellationToken);
            return await ResponseWrapper<UserResponse>.SuccessAsync(identityUserId);
        }
    }
}
