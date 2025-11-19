using Lms.Shared.Application.CustomMediator.Interfaces.Mediator;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lms.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        //private ISender _sender = null!;
        //public ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();

        // Custom Mediator
        private ICustomMediator _customMediator = null;
        public ICustomMediator CustomMediator => _customMediator ??= HttpContext.RequestServices.GetRequiredService<ICustomMediator>();

    }
}
 