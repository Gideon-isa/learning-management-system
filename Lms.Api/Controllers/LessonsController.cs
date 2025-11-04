using Lms.Api.Contracts.Lessons;
using Lms.Api.Extensions;
using Lms.CourseManagement.Application.Features.LessonFeatures.Query;
using Lms.CourseManagement.Application.Features.LessonFeatures.Quries;
using Lms.Shared.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lms.Api.Controllers
{
    [Route("api/[controller]")]
    public class LessonsController : BaseApiController
    {
        [AllowAnonymous]
        [Consumes("multipart/form-data")]
        [HttpPost]
        public async Task<IActionResult> CreateModule([FromForm] CreateLessonRequest createLessonRequest,
            [FromServices] ICommandDispatcher commandDispatcher, CancellationToken cancellationToken)
        {
            var cmd = createLessonRequest.ToCreateCommand();
            await commandDispatcher.DispatcherAsync(cmd, cancellationToken);

            var response = await CustomMediator.Send(cmd);

            if (response.IsSuccessful)
            {
                return Ok(response);
                //return CreatedAtAction(nameof(CreateModule), new { id = response.Data.CourseId });
            }
            return BadRequest(response);
        }

        [HttpGet("{lessonId}")]
        public async Task<IActionResult> GetLessonById([FromRoute] Guid lessonId,
            [FromServices] ICommandDispatcher commandDispatcher, CancellationToken cancellationToken)
        {
            var query = new GetLessonByIdQuery { Id = lessonId };
            await commandDispatcher.DispatcherAsync(query, cancellationToken);
            var response = await CustomMediator.Send(query);
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("{lessonId}/videos/{videoTitle}")]
        public async Task<IActionResult> PlayVideo([FromRoute] Guid lessonId, string videoTitle, 
            [FromServices] ICommandDispatcher commandDispatcher, CancellationToken cancellationToken)
        {
            var query = new StreamLessonVideoQuery { LessonId = lessonId, VideoTitle = videoTitle };
            await commandDispatcher.DispatcherAsync(query, cancellationToken);
            var response = await CustomMediator.Send(query);
            if (response.IsSuccessful)
            {
                var video = response.Data;
                if (!System.IO.File.Exists(video.Path))
                {
                    return NotFound("Video file not found");
                }
                var videoSream = System.IO.File.OpenRead(video.Path);
                return File(videoSream, video.ContentType, video.FileName, enableRangeProcessing: true);
            }
            return BadRequest(response);
        }
    }


}
