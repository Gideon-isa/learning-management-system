using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using MediatR;

namespace Lms.Identity.Application.Features.Identity.Users.Queries.GetUser
{
    internal class GetUserEmailQueryHandler : ICustomRequestHandler<GetUserByEmailQuery, IResponseWrapper>
    {
        private readonly IUserService _userService;
        public GetUserEmailQueryHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IResponseWrapper> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            //var response = await _userService.GetByEmailAsync(request, cancellationToken);
            //return await ResponseWrapper<UserResponse>.SuccessAsync(response);
            throw new NotImplementedException();
        }
    }
}
