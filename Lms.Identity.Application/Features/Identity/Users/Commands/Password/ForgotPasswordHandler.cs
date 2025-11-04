using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using MediatR;

namespace Lms.Identity.Application.Features.Identity.Users.Commands.Password
{
    public class ForgotPasswordHandler : ICustomRequestHandler<ForgotPasswordCommand, IResponseWrapper>
    {
        private readonly IUserService _userService;
        public ForgotPasswordHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IResponseWrapper> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var response = await _userService.ForgotPasswordAsync(request, cancellationToken);
            return ResponseWrapper.Success(response);
        }
    }
}
