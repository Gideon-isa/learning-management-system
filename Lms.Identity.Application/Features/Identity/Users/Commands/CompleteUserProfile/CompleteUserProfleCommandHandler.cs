using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.Identity.Application.Features.Identity.Users.Commands.CompleteUserProfile
{
    public class CompleteUserProfleCommandHandler : ICustomRequestHandler<CompleteUserProfileCommand, IResponseWrapper<UserResponse>>
    {
        private IUserService _userService;

        public CompleteUserProfleCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IResponseWrapper<UserResponse>> Handle(CompleteUserProfileCommand request, CancellationToken cancellationToken)
        {
            var userDto = await _userService.UpdateUserAsync(request, cancellationToken);
            return await ResponseWrapper<UserResponse>.SuccessAsync(data: userDto, ["Updated successfully"]);
            throw new NotImplementedException();
        }
    }
}
