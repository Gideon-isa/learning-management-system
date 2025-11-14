using Lms.Api.Contracts.Enrollment;
using Lms.Enrollment.Application.Features.CourseEnrollment.Command;
using Lms.Enrollment.Application.Features.CourseEnrollment.Queries;
using Lms.Shared.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Lms.Api.Controllers
{
    [Route("api/[controller]")]
    public class CourseEnrollmentsController : BaseApiController
    {
        [HttpPost("{courseId}/")]
        public async Task<IActionResult> EnrollStudentsAsync([FromBody] CreateStudentEnrollmentRequest request, Guid courseId, 
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
                return Ok(response);
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
            }
            return BadRequest(response);
        }

        [HttpPut("{enrollmentId}/students/{studentId}/unenroll-student")]
        public async Task<IActionResult> UnEnrollStudentsAsync([FromRoute] Guid enrollmentId, Guid studentId,
            [FromServices] ICommandDispatcher commandDispatcher, CancellationToken cancellationToken)
        {
            var cmd = new UnEnrollStudentCommand { EnrollmentId = enrollmentId, StudentId = studentId };
            await commandDispatcher.DispatcherAsync(cmd, cancellationToken);

            var response = await CustomMediator.Send(cmd, cancellationToken);

            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEnrollment([FromServices] ICommandDispatcher commandDispatcher, CancellationToken cancellationToken)
        {
            var query = new GetAllEnrollmentsQuery();
            await commandDispatcher.DispatcherAsync(query, cancellationToken);

            var response = await CustomMediator.Send(query, cancellationToken);

            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
