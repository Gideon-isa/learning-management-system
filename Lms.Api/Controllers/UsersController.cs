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
        /// <summary>
        /// Sign up new user
        /// </summary>
        /// <param name="userRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SignUpAsync([FromBody] RegisterUserRequest userRequest, CancellationToken cancellationToken)
        {
            var response = await CustomMediator.Send(new RegisterUserCommand { RegisterUserRequest = userRequest }, cancellationToken);
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Retrieve unique user
        /// </summary>
        /// <param name="userIdOrEmail"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{userIdOrEmail}")]
        public async Task<IActionResult> GetUserAsync([FromRoute] string userIdOrEmail, CancellationToken cancellationToken)
        {
            
            var response = await CustomMediator.Send(new GetUserQuery { UserIdOrEmail = userIdOrEmail }, cancellationToken);
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Initiate forgot password
        /// </summary>
        /// <param name="forgotPasswordRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordRequest forgotPasswordRequest, CancellationToken cancellationToken)
        {
            var response = await CustomMediator.Send(new ForgotPasswordCommand { Request = forgotPasswordRequest }, cancellationToken);
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Initiate reset user password
        /// </summary>
        /// <param name="resetPasswordRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequest resetPasswordRequest, CancellationToken cancellationToken)
        {
            var response = await CustomMediator.Send(new ResetPasswordCommand { Request = resetPasswordRequest }, cancellationToken);
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Change user Password
        /// </summary>
        /// <param name="changePasswordRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordRequest changePasswordRequest, CancellationToken cancellationToken)
        {
            var response = await CustomMediator.Send(new ChangePasswordCommand { Request = changePasswordRequest }, cancellationToken);
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var response = await CustomMediator.Send(new GetAllUsersQuery { }, cancellationToken);
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("active")]
        public async Task<IActionResult> GetAllActiveUsersAsync(CancellationToken cancellationToken)
        {
            var response = await CustomMediator.Send(new GetAllActiveUsersQuery { }, cancellationToken);
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string userId, CancellationToken cancellationToken)
        {
            var response = await CustomMediator.Send(new DeleteUserCommand { UserId = userId }, cancellationToken);
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userIdOrEmail"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("instructor-requests")]
        public async Task<IActionResult> GetInstructorRequestAsync(CancellationToken cancellationToken)
        {
            var response = await CustomMediator.Send(new GetUserInstructorsQuery { }, cancellationToken);
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
        public async Task<IActionResult> InstructorApprovalAsync([FromBody]InstructorApprovalRequest request, string userIdOrEmail, CancellationToken cancellationToken)
        {
            var command = new InstructorApprovalCommand { ApprovalStatus = request.IsApproved, UserIdOrEmail = userIdOrEmail };
            var response = await CustomMediator.Send(command, cancellationToken);
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Complete user's profile
        /// </summary>
        /// <param name="userIdOrEmail"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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
