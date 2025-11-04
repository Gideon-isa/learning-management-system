using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using MediatR;

namespace Lms.Identity.Application.Features.Identity.Users.Commands.DeleteUser
{
    public class DeleteUserByIdCommandHandler : ICustomRequestHandler<DeleteUserCommand, IResponseWrapper>
    {
        private readonly IUserService _userService;
        public DeleteUserByIdCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IResponseWrapper> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var response = await _userService.DeleteAsync(request.UserId, cancellationToken);
            return await ResponseWrapper.SuccessAsync(response);
        }
    }
}
