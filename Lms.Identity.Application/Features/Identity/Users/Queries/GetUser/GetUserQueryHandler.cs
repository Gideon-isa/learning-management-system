using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using MediatR;

namespace Lms.Identity.Application.Features.Identity.Users.Queries.GetUser
{
    public class GetUserQueryHandler : ICustomRequestHandler<GetUserQuery, IResponseWrapper>
    {
        private readonly IUserService _userService;
        public GetUserQueryHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IResponseWrapper> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var response = await _userService.GetUserAsync(request, cancellationToken);
            return await ResponseWrapper<UserResponse>.SuccessAsync(response);
        }
    }
}
