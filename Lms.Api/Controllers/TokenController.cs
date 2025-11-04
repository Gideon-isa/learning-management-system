using Lms.Identity.Application.Features.Identity.Tokens;
using Lms.Identity.Application.Features.Identity.Tokens.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace Lms.Api.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : BaseApiController
    {
        [HttpPost("login")]
        [AllowAnonymous]
        [OpenApiOperation("Used to obtain jwt for login")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequest tokenRequest)
        {
            var response = await CustomMediator.Send(new GetTokenQuery { TokenRequest = tokenRequest });
            
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost("refresh-token")]
        [OpenApiOperation("Used to generate new jwt from refresh token")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRefreshTokenAsync([FromBody] RefreshTokenRequest refreshTokenokenRequest)
        {
            var response = await CustomMediator.Send(new GetRefreshTokenQuery { RefreshToken = refreshTokenokenRequest });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
