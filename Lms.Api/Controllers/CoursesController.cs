using Lms.Api.Contracts.Courses;
using Lms.CourseManagement.Application.Features.Course.Commands.PublishCourse;
using Lms.CourseManagement.Application.Features.Course.Queries.GetAllCourse;
using Lms.CourseManagement.Application.Features.Course.Queries.GetCourseById;
using Lms.CourseManagement.Application.Features.CourseFeatures.Commands.CreateCourse;
using Lms.Shared.Application.Contracts;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lms.Api.Controllers
{
    [Route("api/[controller]")]
    public class CoursesController : BaseApiController
    {

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateCourseAsync([FromBody] CreateCourseRequest courseRequest,
            [FromServices] ICommandDispatcher commandDispatcher, CancellationToken cancellationToken)
        {
            var cmd = courseRequest.Adapt<CreateCourseCommand>();
            await commandDispatcher.DispatcherAsync(cmd, cancellationToken);

            var response = await CustomMediator.Send(cmd, cancellationToken);
            if (response.IsSuccessful)
            {
                return CreatedAtAction(nameof(GetCourseById), "Courses" ,new { courseId = response.Data.Id }, response.Data);
                //return Ok(new { url = Url.Action(nameof(GetCourseById), new { courseId = response.Data.Id }) });
            }
            return BadRequest(response);
        }

        [AllowAnonymous]
        [HttpPut("{courseId}/publish")]
        public async Task<IActionResult> PublishCourse([FromRoute] Guid courseId,
            [FromServices] ICommandDispatcher commandDispatcher, CancellationToken cancellationToken)
        {
            var cmd = new PublishCourseCommand { CourseId = courseId };
            await commandDispatcher.DispatcherAsync(cmd, cancellationToken);

            var response = await CustomMediator.Send(cmd, cancellationToken);

            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [AllowAnonymous]
        [HttpGet("{courseId}")]
        public async Task<IActionResult> GetCourseById([FromRoute] Guid courseId, 
            [FromServices] ICommandDispatcher commandDispatcher ,CancellationToken cancellationToken)
        {
            var query = new GetCourseByIdQuery { CourseId = courseId};
            await commandDispatcher.DispatcherAsync(query, cancellationToken);

            var response = await CustomMediator.Send(query);

            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetCourses(
            [FromServices] ICommandDispatcher commandDispatcher, CancellationToken cancellationToken)
        {;
            var query = new GetCoursesQuery();
            await commandDispatcher.DispatcherAsync(query, cancellationToken);

            var response = await CustomMediator.Send(query);

            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [AllowAnonymous]
        [HttpDelete("{courseId}")]
        public async Task<IActionResult> DeleteCourse([FromRoute] Guid courseId,
            [FromServices] ICommandDispatcher commandDispatcher, CancellationToken cancellationToken)
        {
            var cmd = courseId.Adapt<PublishCourseCommand>();
            await commandDispatcher.DispatcherAsync(cmd, cancellationToken);

            var response = await CustomMediator.Send(cmd);

            if (response.IsSuccessful)
            {
                return NoContent();
            }
            return BadRequest(response);
        }
    }
}
