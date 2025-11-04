using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using MediatR;

namespace Lms.Identity.Application.Features.Identity.Tokens.Queries
{
    public class GetRefreshTokenQuery : ICustomRequest<IResponseWrapper>
    {
        public RefreshTokenRequest RefreshToken { get; set; } = default!;
    }

    // public class GetRefreshTokenQuery : IRequest<IResponseWrapper>
}


