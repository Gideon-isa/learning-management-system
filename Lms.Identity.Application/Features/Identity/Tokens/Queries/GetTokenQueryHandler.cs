using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using MediatR;

namespace Lms.Identity.Application.Features.Identity.Tokens.Queries
{
    public class GetTokenQueryHandler : ICustomRequestHandler<GetTokenQuery, IResponseWrapper>
    {
        private readonly ITokenService _tokenService;
        public GetTokenQueryHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        public async Task<IResponseWrapper> Handle(GetTokenQuery request, CancellationToken cancellationToken)
        {
            var token = await _tokenService.LoginAsync(request);
            return await ResponseWrapper<TokenResponse>.SuccessAsync(token);
        }

        // public class GetTokenQueryHandler : IRequestHandler<GetTokenQuery, IResponseWrapper>
    }
}
