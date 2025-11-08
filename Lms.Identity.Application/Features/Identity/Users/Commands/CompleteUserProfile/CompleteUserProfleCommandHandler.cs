using Lms.Identity.Application.Abstractions;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using Lms.SharedKernel.Domain.Enums;

namespace Lms.Identity.Application.Features.Identity.Users.Commands.CompleteUserProfile
{
    public class CompleteUserProfleCommandHandler : ICustomRequestHandler<CompleteUserProfileCommand, IResponseWrapper<UserResponse>>
    {
        private readonly IUserService _userService;
        private readonly IIdentityUnitOfWork _identityUnitOfWork;

        public CompleteUserProfleCommandHandler(IUserService userService, IIdentityUnitOfWork identityUnitOfWork)
        {
            _userService = userService;
            _identityUnitOfWork = identityUnitOfWork;
        }

        public async Task<IResponseWrapper<UserResponse>> Handle(CompleteUserProfileCommand request, CancellationToken cancellationToken)
        {
            var userDto = await _userService.CompleteProfileAsync(request, cancellationToken);
            await _identityUnitOfWork.SaveChangesAsync(cancellationToken);
            return await ResponseWrapper<UserResponse>.SuccessAsync(data: userDto, ["Updated successfully"]);
        }
    }
}
