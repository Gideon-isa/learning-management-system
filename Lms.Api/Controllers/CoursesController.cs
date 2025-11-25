using Lms.Api.Contracts.Courses;
using Lms.CourseManagement.Application.Features.Course.Commands.PublishCourse;
using Lms.CourseManagement.Application.Features.Course.Queries.GetAllCourse;
using Lms.CourseManagement.Application.Features.Course.Queries.GetCourseById;
using Lms.CourseManagement.Application.Features.CourseFeatures.Commands.CreateCourse;
using Lms.Identity.Infrastructure.Identity.Auth;
using Lms.Shared.Application.Contracts;
using Lms.Shared.Security.Permissions;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Lms.Api.Controllers
{
    [Route("api/[controller]")]
    public class CoursesController : BaseApiController
    {
        /// <summary>
        /// Create a course resource
        /// </summary>
        /// <param name="courseRequest"></param>
        /// <param name="commandDispatcher"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        //[ShouldHavePermission(PermissionConstants.Create, LearningFeatureConstants.Course)]
        [HttpPost]
        public async Task<IActionResult> CreateCourseAsync([FromBody] CreateCourseRequest courseRequest,
            [FromServices] ICommandDispatcher commandDispatcher, CancellationToken cancellationToken)
        {
            var cmd = courseRequest.Adapt<CreateCourseCommand>();
            await commandDispatcher.DispatcherAsync(cmd, cancellationToken);
            var response = await CustomMediator.Send(cmd, cancellationToken);
            if (response.IsSuccessful)
                return CreatedAtAction(nameof(GetCourseById), "Courses", new { courseId = response.Data.Id }, response.Data);
            return BadRequest(response);
        }

        /// <summary>
        /// Publish a course to the Enrollment module
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ShouldHavePermission(PermissionConstants.Update, LearningFeatureConstants.Course)]
        [HttpPut("{courseId}/publish")]
        public async Task<IActionResult> PublishCourse([FromRoute] Guid courseId, CancellationToken cancellationToken)
        {
            var cmd = new PublishCourseCommand { CourseId = courseId };
            var response = await CustomMediator.Send(cmd, cancellationToken);
            if (response.IsSuccessful)
                return Ok(response);    
            return BadRequest(response);
        }

        /// <summary>
        /// Retrieve a course using its unique identifier
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ShouldHavePermission(PermissionConstants.Read, LearningFeatureConstants.Course)]
        [HttpGet("{courseId}")]
        public async Task<IActionResult> GetCourseById([FromRoute] Guid courseId, CancellationToken cancellationToken)
        {
            var query = new GetCourseByIdQuery { CourseId = courseId};
            var response = await CustomMediator.Send(query, cancellationToken);
            if (response.IsSuccessful)
                return Ok(response);
            return BadRequest(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="getCoursesRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        //[ShouldHavePermission(PermissionConstants.Read, LearningFeatureConstants.Course)]
        [HttpGet]
        public async Task<IActionResult> GetCourses([FromQuery] GetCoursesRequest getCoursesRequest, CancellationToken cancellationToken)
        {
            var query = getCoursesRequest.Adapt<GetCoursesQuery>();
            var response = await CustomMediator.Send(query, cancellationToken);
            if (response.IsSuccessful)
                return Ok(response);
            return BadRequest(response);
        }

        /// <summary>
        /// Delete a course resource
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ShouldHavePermission(PermissionConstants.Delete, LearningFeatureConstants.Course)]
        [HttpDelete("{courseId}")]
        public async Task<IActionResult> DeleteCourse([FromRoute] Guid courseId, CancellationToken cancellationToken)
        {
            var cmd = courseId.Adapt<PublishCourseCommand>();
            var response = await CustomMediator.Send(cmd, cancellationToken);
            if (response.IsSuccessful)
                return NoContent();
            return BadRequest(response);
        }
    }
}
