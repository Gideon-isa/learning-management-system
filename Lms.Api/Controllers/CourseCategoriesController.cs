using Lms.Api.Contracts.CourseCategory;
using Lms.CourseManagement.Application.Features.CourseCategory.Command.CreateCategory;
using Lms.CourseManagement.Application.Features.CourseCategory.Queries.GetAllCategories;
using Lms.CourseManagement.Application.Features.CourseCategory.Queries.GetCategory;
using Lms.Shared.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Lms.Api.Controllers
{
    [Route("api/[controller]")]
    public class CourseCategoriesController : BaseApiController
    {
        /// <summary>
        /// Creates a new course category.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation. Returns an HTTP 200 response with a
        /// success message if the course category is created successfully.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateCourseCategory([FromBody] CreateCourseCategoryRequest request, [FromServices] ICommandDispatcher commandDispatcher, CancellationToken cancellationToken)
        {
            var cmd = new CreateCourseCategoryCommand { Name = request.Name };
            await commandDispatcher.DispatcherAsync(cmd, cancellationToken);
            var response = await CustomMediator.Send(cmd, cancellationToken);
            if (response.IsSuccessful)
                return CreatedAtAction(nameof(GetCourseCategoryById), "CourseCategory", new { courseId = response.Data }, response.Data);
            return BadRequest(response);
        }

        /// <summary>
        /// Retrieves the details of a course category by its unique identifier.
        /// </summary>
        /// <param name="courseCategoryId">The unique identifier of the course category to retrieve.</param>
        /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
        /// <returns>An IActionResult containing the course category details if found; otherwise, a BadRequest result with error
        /// information.</returns>
        [HttpGet]
        [Route("{courseCategoryId}")]
        public async Task<IActionResult> GetCourseCategoryById([FromQuery] Guid courseCategoryId, CancellationToken cancellationToken)
        {
            var query = new GetCourseCategoryQuery { Id = courseCategoryId };
            var response = await CustomMediator.Send(query, cancellationToken);
            if (response.IsSuccessful)
                return Ok(response);
            return BadRequest(response);
        }

        /// <summary>
        /// Retrieves all available course categories.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>An <see cref="IActionResult"/> containing the result of the operation. Returns an HTTP 200 response with the
        /// course categories if successful; otherwise, returns an HTTP 400 response with error details.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCourseCategories(CancellationToken cancellationToken)
        {
            var query = new GetCourseCategoriesQuery { };
            var response = await CustomMediator.Send(query, cancellationToken);
            if (response.IsSuccessful)
                return Ok(response);
            return BadRequest(response);
        }
    }
}
