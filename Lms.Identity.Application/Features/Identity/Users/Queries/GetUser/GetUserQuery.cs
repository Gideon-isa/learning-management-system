using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using MediatR;

namespace Lms.Identity.Application.Features.Identity.Users.Queries.GetUser
{
    public class GetUserQuery : ICustomRequest<IResponseWrapper>
    {
        public string UserIdOrEmail { get; set; }
    }
    
}
