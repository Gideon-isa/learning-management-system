using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using MediatR;

namespace Lms.Identity.Application.Features.Identity.Users.Commands.Password
{
    public class ResetPasswordCommandHandler : ICustomRequestHandler<ResetPasswordCommand, IResponseWrapper>
    {
        private readonly IUserService _userService;
        public ResetPasswordCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IResponseWrapper> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var response = await _userService.ResetPasswordAsync(request, cancellationToken);
            return ResponseWrapper.Success(response);
        }
    }
}
