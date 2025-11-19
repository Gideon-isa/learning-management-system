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
        /// 
        /// </summary>
        /// <param name="createCourseModuleReq"></param>
        /// <param name="commandDispatcher"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateModule([FromBody] CreateCourseModuleRequest createCourseModuleReq,
            [FromServices] ICommandDispatcher commandDispatcher, CancellationToken cancellationToken)
        {
            var cmd = createCourseModuleReq.Adapt<CreateCourseModuleCommand>();
            await commandDispatcher.DispatcherAsync(cmd, cancellationToken);

            var response = await CustomMediator.Send(cmd);

            if (response.IsSuccessful)
            {
                return CreatedAtAction(nameof(CreateModule), new { id = response.Data.Id });
            }
            return BadRequest(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="courseId"></param>
        /// <param name="commandDispatcher"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{moduleId}")]
        public async Task<IActionResult> GetModuleById([FromRoute] Guid moduleId, [FromRoute] Guid courseId,
            [FromServices] ICommandDispatcher commandDispatcher, CancellationToken cancellationToken)
        {
            var query = new GetCourseModuleByIdQuery { ModuleId = moduleId, CourseId = courseId};
            await commandDispatcher.DispatcherAsync(query, cancellationToken);
            var response = await CustomMediator.Send(query);
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
    
}