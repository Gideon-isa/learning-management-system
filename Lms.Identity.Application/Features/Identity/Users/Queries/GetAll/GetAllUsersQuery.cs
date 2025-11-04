using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using MediatR;

namespace Lms.Identity.Application.Features.Identity.Users.Queries.GetAll
{
    public class GetAllUsersQuery : ICustomRequest<IResponseWrapper>
    {
    }
}
