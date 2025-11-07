using Lms.Api.Contracts.Enrollment;
using Lms.CourseManagement.Application.Features.Module.Commands;
using Lms.Enrollment.Application.Features.CourseEnrollment.Command;
using Lms.Enrollment.Application.Features.CourseEnrollment.Queries;
using Lms.Identity.Application.Features.Identity.Users.Queries.GetAll;
using Lms.Shared.Application.Contracts;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lms.Api.Controllers
{
    [Route("api/[controller]")]
    public class CourseEnrollmentsController : BaseApiController
    {
        [HttpPost("{courseId}/")]
        public async Task<IActionResult> EnrollStudentAsync([FromBody] CreateStudentEnrollmentRequest request, Guid courseId, 
            [FromServices] ICommandDispatcher commandDispatcher, CancellationToken cancellationToken)
        {
            var cmd = new CreateEnrollmentCommand { CourseId = courseId, StudentIds = request.StudentIds }; 
            await commandDispatcher.DispatcherAsync(cmd, cancellationToken);

            var response = await CustomMediator.Send(cmd, cancellationToken);

            if (response.IsSuccessful)
            {
                //return Ok(response.Data);
                return CreatedAtAction(nameof(GetCourseEnrollmentById), "CourseEnrollments", new { enrollmentId = response.Data.Id }, response);
            }
            return BadRequest(response);
        }

        [HttpGet("{enrollmentId}")]
        public async Task<IActionResult> GetCourseEnrollmentById(Guid enrollmentId, [FromServices] ICommandDispatcher commandDispatcher, CancellationToken cancellationToken)
        {
            var query = new GetEnrollmentByIdQuery { Id =  enrollmentId };

            var response = await CustomMediator.Send(query, cancellationToken);

            if (response.IsSuccessful)
            {
                return Ok(response.Data);
                //return CreatedAtAction(nameof(CreateModule), new { id = response.Data.Id });
            }
            return BadRequest(response);
        }

        [HttpGet("available-courses")]
        public async Task<IActionResult> GetAllAvailableCourses([FromServices] ICommandDispatcher commandDispatcher, CancellationToken cancellationToken)
        {
            var query = new GetAllAvailableCoursesQuery();
            var response = await CustomMediator.Send(query, cancellationToken);

            if (response.IsSuccessful)
            {
                return Ok(response.Data);
                //return CreatedAtAction(nameof(CreateModule), new { id = response.Data.Id });
            }
            return BadRequest(response);

        }

        [HttpGet("available-students")]
        public async Task<IActionResult> GetAllAvailableStudents([FromServices] ICommandDispatcher commandDispatcher, CancellationToken cancellationToken)
        {
            var query = new GetAllAvailableStudentQuery();
            var response = await CustomMediator.Send(query, cancellationToken);

            if (response.IsSuccessful)
            {
                return Ok(response.Data);
                //return CreatedAtAction(nameof(CreateModule), new { id = response.Data.Id });
            }
            return BadRequest(response);

        }
    }
}
