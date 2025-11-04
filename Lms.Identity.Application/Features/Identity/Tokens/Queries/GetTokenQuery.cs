using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using MediatR;

namespace Lms.Identity.Application.Features.Identity.Tokens.Queries
{
    public class GetTokenQuery : ICustomRequest<IResponseWrapper>
    {
        public TokenRequest TokenRequest { get; set; }
    }

    // public class GetTokenQuery : IRequest<IResponseWrapper>
}
