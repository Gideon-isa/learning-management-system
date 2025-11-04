using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.Identity.Application.Features.Identity.Users.Queries.GetAllUserInstructor
{
    public class GetUserInstructorsQuery : ICustomRequest<IResponseWrapper<UserResponses>>
    {
    }
}
