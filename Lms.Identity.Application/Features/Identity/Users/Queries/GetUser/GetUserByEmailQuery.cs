using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using MediatR;

namespace Lms.Identity.Application.Features.Identity.Users.Queries.GetUser
{
    public class GetUserByEmailQuery : ICustomRequest<IResponseWrapper>
    {
        public string UserEmail { get; set; }
    }
}
