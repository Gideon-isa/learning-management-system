using Lms.ContentDelivery.Application.Features.Queries.ContentDelivery;
using Lms.ContentDelivery.Application.Features.Queries.StudentModule;
using Lms.Shared.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Lms.Api.Controllers
{
    [Route("api/[controller]")]
    [Consumes("multipart/form-data")]
    public class ContentDeliveriesController : BaseApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentCode"></param>
        /// <param name="commandDispatcher"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="commandDispatcher"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("vidoes/{videoId}")]
        public async Task<IActionResult> PlayVideo([FromRoute] Guid videoId, [FromServices] ICommandDispatcher commandDispatcher, CancellationToken cancellationToken)
        {
            var query = new GetVideoResourceByIdQuery { VideoId = videoId };
            await commandDispatcher.DispatcherAsync(query, cancellationToken);
            var response = await CustomMediator.Send(query, cancellationToken);
            if (response.IsSuccessful)
            {
                var video = response.Data;
                if (!System.IO.File.Exists(video.Path))
                { 
                    return NotFound();
                }
                var videoStream = System.IO.File.OpenRead(video.Path);

                return File(videoStream, video.ContentType, video.FileName, enableRangeProcessing: true);
            }
            return BadRequest(response);


        }
    }
}
