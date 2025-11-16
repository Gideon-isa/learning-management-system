using Lms.Identity.Application.Features.Identity.Users.Commands.AddInstructor;
using Lms.Identity.Application.Features.Identity.Users.Commands.AddUser;
using Lms.Identity.Application.Features.Identity.Users.Commands.CompleteUserProfile;
using Lms.Identity.Application.Features.Identity.Users.Commands.DeleteUser;
using Lms.Identity.Application.Features.Identity.Users.Commands.InstructorApproval;
using Lms.Identity.Application.Features.Identity.Users.Commands.Password;
using Lms.Identity.Application.Features.Identity.Users.Queries.GetAll;
using Lms.Identity.Application.Features.Identity.Users.Queries.GetAllUserInstructor;
using Lms.Identity.Application.Features.Identity.Users.Queries.GetUser;
using Lms.Identity.Application.Features.Identity.Users.Requests;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lms.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : BaseApiController
    {

        [HttpPost]
        public async Task<IActionResult> SignUpAsync([FromBody] RegisterUserRequest userRequest, CancellationToken cancellationToken)
        {
            var response = await CustomMediator.Send(new RegisterUserCommand { RegisterUserRequest = userRequest });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("{userIdOrEmail}")]
        public async Task<IActionResult> GetUserAsync([FromRoute] string userIdOrEmail, CancellationToken cancellationToken)
        {
            
            var response = await CustomMediator.Send(new GetUserQuery { UserIdOrEmail = userIdOrEmail });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordRequest forgotPasswordRequest, CancellationToken cancellationToken)
        {
            var response = await CustomMediator.Send(new ForgotPasswordCommand { Request = forgotPasswordRequest });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequest resetPasswordRequest, CancellationToken cancellationToken)
        {
            var response = await CustomMediator.Send(new ResetPasswordCommand { Request = resetPasswordRequest });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest, CancellationToken cancellationToken)
        {
            var response = await CustomMediator.Send(new ChangePasswordCommand { Request = changePasswordRequest });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var response = await CustomMediator.Send(new GetAllUsersQuery { });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetAllActiveUsersAsync(CancellationToken cancellationToken)
        {
            var response = await CustomMediator.Send(new GetAllActiveUsersQuery { });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string userId, CancellationToken cancellationToken)
        {
            var response = await CustomMediator.Send(new DeleteUserCommand { UserId = userId });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost("{userIdOrEmail}/request-instructor")]
        public async Task<IActionResult> MakeUserInstructorAsync([FromRoute] string userIdOrEmail, CancellationToken cancellationToken)
        {
            var response = await CustomMediator.Send(new AddInstructorCommand { UserIdOrEmail = userIdOrEmail }, cancellationToken);
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [AllowAnonymous]
        [HttpGet("instructor-requests")]
        public async Task<IActionResult> GetInstructorRequestAsync(CancellationToken cancellationToken)
        {
            var response = await CustomMediator.Send(new GetUserInstructorsQuery { });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);

        }

        /// <summary>
        /// Approving or Rejecting the user's request as an Instructor 
        /// </summary>
        /// <param name="request">request body containg the user approval or rejection</param>
        /// <param name="cancellationToken"></param>
        /// <returns>a string</returns>
        [AllowAnonymous]
        [HttpPost("{userIdOrEmail}/instructor-approval")]
        public async Task<IActionResult> InstructorApprovalAsync(InstructorApprovalRequest request, string userIdOrEmail, CancellationToken cancellationToken)
        {
            var command = new InstructorApprovalCommand { ApprovalStatus = request.IsApproved, UserIdOrEmail = userIdOrEmail };
            var response = await CustomMediator.Send(command, cancellationToken);
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [AllowAnonymous]
        [HttpPut("/{userIdOrEmail}/complete-User-Profile")]
        public async Task<IActionResult> CompleteUserProfileAsync([FromRoute] string userIdOrEmail, 
            [FromBody] CompleteUserProfileRequest request, CancellationToken cancellationToken)
        {
            var command = request.Adapt<CompleteUserProfileCommand>();
            command.Id = userIdOrEmail;

            var response = await CustomMediator.Send(command, cancellationToken);
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);

        }
    }
}
