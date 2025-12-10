using Lms.Api.Contracts.CourseModule;
using Lms.CourseManagement.Application.Features.Module.Commands;
using Lms.CourseManagement.Application.Features.Module.Queries;
using Lms.Shared.Application.Contracts;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lms.Api.Controllers
{
    [Route("api/courses/{courseId}/modules")]
    public class CourseModulesController : BaseApiController
    {
        /// <summary>
        /// Create a course module
        /// </summary>
        /// <param name="createCourseModuleReq"></param>
        /// <param name="commandDispatcher"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateModule([FromRoute] Guid courseId, [FromBody] CreateCourseModuleRequest createCourseModuleReq,
            [FromServices] ICommandDispatcher commandDispatcher, CancellationToken cancellationToken)
        {
            var cmd = createCourseModuleReq.Adapt<CreateCourseModuleCommand>();
            cmd.CourseId = courseId;
            await commandDispatcher.DispatcherAsync(cmd, cancellationToken);
            var response = await CustomMediator.Send(cmd, cancellationToken);

            if (response.IsSuccessful)
            {
                return CreatedAtAction(nameof(CreateModule), new { id = response.Data.Id }, response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Retrieve a course module
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="courseId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{moduleId}")]
        public async Task<IActionResult> GetModuleById([FromRoute] Guid moduleId, [FromRoute] Guid courseId, CancellationToken cancellationToken)
        {
            var query = new GetCourseModuleByIdQuery { ModuleId = moduleId, CourseId = courseId};
            var response = await CustomMediator.Send(query, cancellationToken);
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Delete a course module
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="courseId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpDelete("{moduleId}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid moduleId, [FromRoute] Guid courseId, CancellationToken cancellationToken)
        {
            var cmd = new DeleteCourseModuleCommand { ModuleId = moduleId, CourseId = courseId };
            var response = await CustomMediator.Send(cmd, cancellationToken);
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
    
}