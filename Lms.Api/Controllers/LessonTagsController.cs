using Lms.Api.Contracts.LessonTags;
using Lms.CourseManagement.Application.Features.Tags.Command;
using Lms.CourseManagement.Application.Features.Tags.Queries;
using Lms.Shared.Application.Contracts;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Lms.Api.Controllers
{
    [Route("api/[controller]")]
    public class LessonTagsController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateLessonTag([FromBody] CreateLessonTagsRequest request, 
            [FromServices] ICommandDispatcher commandDispatcher, CancellationToken cancellationToken)
        {
            var command = request.Adapt<CreateLessonTagCommand>();

            await commandDispatcher.DispatcherAsync(command, cancellationToken);
            var response = await CustomMediator.Send(command);

            if (response.IsSuccessful)
            {
                return CreatedAtAction(nameof(GetLeessonTagById), "LessonTags", new { tagId = response.Data.Id }, response.Data);
            }
            return BadRequest(request);
        }

        [HttpGet("{tagId}")]
        public async Task<IActionResult> GetLeessonTagById([FromRoute] Guid tagId,
            [FromServices] ICommandDispatcher commandDispatcher, CancellationToken cancellationToken)
        {
            var query = new GetLessonTagByIdQuery { Id = tagId };
            await commandDispatcher.DispatcherAsync(query, cancellationToken);
            var response = await CustomMediator.Send(query);
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLessons([FromServices] ICommandDispatcher commandDispatcher, CancellationToken cancellationToken)
        {
            var query = new GetAllLessonTagsQuery{ };
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
