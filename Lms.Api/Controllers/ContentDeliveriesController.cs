using Lms.ContentDelivery.Application.Features.Queries.StudentCourse;
using Lms.Shared.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Lms.Api.Controllers
{
    [Route("api/[controller]")]
    public class ContentDeliveriesController : BaseApiController
    {
        [HttpGet("{studentCode}")]
        public async Task<IActionResult> GetStudentAccess
            ([FromRoute] string studentCode, [FromServices] ICommandDispatcher commandDispatcher, CancellationToken cancellationToken)
        {
            var query = new GetStudentAccessCourseQuery { StudentCode = studentCode };
            await commandDispatcher.DispatcherAsync(query, cancellationToken);
            var response = await CustomMediator.Send(query, cancellationToken);
            if(response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);

        }
    }
}
