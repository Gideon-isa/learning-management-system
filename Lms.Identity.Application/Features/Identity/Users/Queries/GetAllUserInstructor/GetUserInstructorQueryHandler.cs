using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.Identity.Application.Features.Identity.Users.Queries.GetAllUserInstructor
{
    public class GetUserInstructorQueryHandler : ICustomRequestHandler<GetUserInstructorsQuery, IResponseWrapper<UserResponses>>
    {
        private readonly IUserService _userService;

        public GetUserInstructorQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IResponseWrapper<UserResponses>> Handle(GetUserInstructorsQuery request, CancellationToken cancellationToken)
        {
            var responseList = await _userService.GetUserInstructorRequestsAsync(request, cancellationToken);
            var userDto =  new UserResponses { responses = responseList };
            return await ResponseWrapper<UserResponses>.SuccessAsync(data: userDto, ["successful"]);
            
        }
    }
}
