using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using MediatR;

namespace Lms.Identity.Application.Features.Identity.Users.Queries.GetAll
{
    public class GetAllUsersQueryHandler : ICustomRequestHandler<GetAllUsersQuery, IResponseWrapper>
    {
        private readonly IUserService _userService;

        public GetAllUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IResponseWrapper> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var response = await _userService.GetAllAsync(request, cancellationToken);
            return await ResponseWrapper<List<UserResponse>>.SuccessAsync(data: response);
        }
    }
}
