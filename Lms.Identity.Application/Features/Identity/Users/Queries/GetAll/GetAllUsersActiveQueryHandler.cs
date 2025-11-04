using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using MediatR;

namespace Lms.Identity.Application.Features.Identity.Users.Queries.GetAll
{
    public class GetAllUsersActiveQueryHandler : ICustomRequestHandler<GetAllActiveUsersQuery, IResponseWrapper>
    {
        private readonly IUserService _userService;

        public GetAllUsersActiveQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IResponseWrapper> Handle(GetAllActiveUsersQuery request, CancellationToken cancellationToken)
        {
            var response = await _userService.GetAllActiveAsync(request, cancellationToken);
            return await ResponseWrapper<List<UserResponse>>.SuccessAsync(data: response);
            throw new NotImplementedException();
        }
    }
}
