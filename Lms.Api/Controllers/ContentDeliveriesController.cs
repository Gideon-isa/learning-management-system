using Lms.ContentDelivery.Application.Features.Queries.StudentModule;
using Lms.CourseManagement.Application.Features.Module.Queries;
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
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        //[HttpGet("course/{courseId}/module/{moduleId}/lesson")]
        //public async Task<IActionResult> GetModuleAsync(Guid courseId, Guid moduleId, [FromServices] ICommandDispatcher commandDispatcher, CancellationToken cancellationToken)
        //{
        //    var query = new GetMouduleByIdQuery { CourseId = courseId, ModuleId = moduleId };
        //    await commandDispatcher.DispatcherAsync(query, cancellationToken);
        //    var response = await CustomMediator.Send(query, cancellationToken);
        //}

        //[HttpGet("course/{courseId}/module/{moduleId}/lesson/{lessonId}/video/{videoTitle}")]
        //public async Task<IActionResult> PlayVideo([FromRoute] Guid courseId, [FromRoute] Guid moduleId, [FromRoute] Guid lessonId, string videoTitle)
        //{

        //}
    }
}
