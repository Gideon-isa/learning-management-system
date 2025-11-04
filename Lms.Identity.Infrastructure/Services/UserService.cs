using Lms.Identity.Application.Exceptions;
using Lms.Identity.Application.Features.Identity.Users;
using Lms.Identity.Application.Features.Identity.Users.Commands.AddUser;
using Lms.Identity.Application.Features.Identity.Users.Commands.CompleteUserProfile;
using Lms.Identity.Application.Features.Identity.Users.Commands.InstructorApproval;
using Lms.Identity.Application.Features.Identity.Users.Commands.Password;
using Lms.Identity.Application.Features.Identity.Users.Queries.GetAll;
using Lms.Identity.Application.Features.Identity.Users.Queries.GetAllUserInstructor;
using Lms.Identity.Application.Features.Identity.Users.Queries.GetUser;
using Lms.Identity.Infrastructure.Constants;
using Lms.Identity.Infrastructure.Identity;
using Lms.Identity.Infrastructure.Identity.Models;
using Lms.Shared.Application;
using Lms.SharedKernel.Domain.Enums;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text;

namespace Lms.Identity.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly AppOptions _appOptions;

        public UserService(UserManager<ApplicationUser> userManager, IOptions<AppOptions> appOptions, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _appOptions = appOptions.Value;
            _roleManager = roleManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="IdentityException"></exception>
        public async Task<string> ChangePasswordAsync(ChangePasswordCommand command, CancellationToken token)
        {
            var userInDb = await GetActiveUserByEmailAsync(command.Request.Email, token);
            var passwordCheck = await _userManager.CheckPasswordAsync(userInDb, command.Request.CurrentPassword);
            if (!passwordCheck)
            {
                throw new IdentityException(["Current password is incorrect"]);
            }

            var result = await _userManager.ChangePasswordAsync(userInDb, command.Request.CurrentPassword, command.Request.NewPassword);
            EnsureOperationIsSuccessful(result);
            //if (!result.Succeeded)
            //{
            //    throw new IdentityException(IdentityHelper.GetIdentityResultErrorDescriptions(result));
            //}
            return "Password Updated successfully";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<UserResponse> CreateAsync(RegisterUserCommand command, CancellationToken token)
        {
            var newUser = new ApplicationUser
            {
                Email = command.RegisterUserRequest?.Email,
                UserName = command.RegisterUserRequest?.Email,
            };

            var result = await _userManager.CreateAsync(newUser, command.RegisterUserRequest.Password);
            EnsureOperationIsSuccessful(result);
            //if (!result.Succeeded)
            //{
            //    throw new IdentityException(IdentityHelper.GetIdentityResultErrorDescriptions(result));
            //}
            return newUser.Adapt<UserResponse>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string> DeleteAsync(string id, CancellationToken token)
        {
            var userInDb = await _userManager.FindByIdAsync(id);
            EnsureUserIsActive(userInDb);
            //if (userInDb == null || !userInDb.IsActive)
            //{
            //    throw new NotFoundException(["user not found or inactive"]);
            //}
            userInDb.IsActive = false;
            var result = await _userManager.UpdateAsync(userInDb);
            EnsureOperationIsSuccessful(result);
            //if (!result.Succeeded)
            //{
            //    throw new IdentityException(["Some went wrong"]);
            //}
            return "User successfully deleted";

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string> ForgotPasswordAsync(ForgotPasswordCommand request, CancellationToken token)
        {
            var userInDb = await _userManager.FindByEmailAsync(request.Request.Email);
            EnsureUserIsActive(userInDb);
            //if (userInDb == null || !userInDb.IsActive)
            //{
            //    throw new NotFoundException(["User not found or inactive"]);
            //}
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(userInDb);
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(resetToken));
            var resetAppUrl = $"{_appOptions.FrontendAppUrl}/reset-password?email={request.Request.Email}&token={encodedToken}";

            return resetAppUrl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<UserResponse>> GetAllAsync(GetAllUsersQuery command, CancellationToken token)
        {
            var users = await _userManager.Users.IgnoreQueryFilters().ToListAsync(token);
            return users.Adapt<List<UserResponse>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<UserResponse>> GetAllActiveAsync(GetAllActiveUsersQuery query, CancellationToken token)
        {
            var users = await _userManager.Users.ToListAsync(token);
            return users.Adapt<List<UserResponse>>();
        }

        //public async Task<UserResponse> GetByEmailAsync(GetUserByEmailQuery command, CancellationToken token)
        //{
        //    var userInDb = await GetActivetUserByEmailAsync(command.GetUserByEmailRequest.Email, token);
        //    return userInDb.Adapt<UserResponse>();
        //}

        public async Task<UserResponse> GetUserAsync(GetUserQuery query, CancellationToken token)
        {
            var userInDb = await GetActiveUserByEmailAsync(query.UserIdOrEmail, token);
            return userInDb.Adapt<UserResponse>();
            //var isEmail = IsEmailAsync(command.UserIdOrEmail, token);
            //if (isEmail)
            //{ 
                
            //}
            //var userInDb = await _userManager.FindByIdAsync(command.UserIdOrEmail.UserId);
            //EnsureUserIsActive(userInDb);
            ////if (userInDb == null || !userInDb.IsActive)
            ////{
            ////    throw new NotFoundException(["User not found or is inactive"]);
            ////}
            //return userInDb.Adapt<UserResponse>();
        }

        public async Task<string> ResetPasswordAsync(ResetPasswordCommand command, CancellationToken token)
        {
            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(command.Request.Token));
            var userInDb = await GetActiveUserByEmailAsync(command.Request.Email, token);
            var result = await _userManager.ResetPasswordAsync(userInDb, decodedToken, command.Request.NewPassword);
            EnsureOperationIsSuccessful(result);
            //if (!result.Succeeded)
            //{
            //    throw new IdentityException(IdentityHelper.GetIdentityResultErrorDescriptions(result));
            //}
            return "Password has been successfully reset";
        }

        private async Task<ApplicationUser?> GetActiveUserByEmailAsync(string emailorId, CancellationToken token)
        {
            var isEmail = IsEmailAsync(emailorId, token);
            ApplicationUser? userInDb;

            if (isEmail)
            {
                userInDb = await _userManager.FindByEmailAsync(emailorId);
            }
            else
            {
                userInDb = await _userManager.FindByIdAsync(emailorId);
            }

            EnsureUserIsActive(userInDb);
            return userInDb;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailOrId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private bool IsEmailAsync(string emailOrId, CancellationToken token)
        {
            int minimumValidEmailLength = 5; // minimum length of a valid email address
            if (!emailOrId.Contains('@') || emailOrId.Length < minimumValidEmailLength)
            {
                return false;
            }

            try
            {
                var addr = new System.Net.Mail.MailAddress(emailOrId);
                return addr.Address == emailOrId; // compare normalized addresses
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="IdentityException"></exception>
        private void EnsureUserIsActive(ApplicationUser? user)
        {
            if (user == null || !user.IsActive)
            {
                throw new IdentityException(["User not found or is inactive"]);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <exception cref="IdentityException"></exception>
        private void EnsureOperationIsSuccessful(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                throw new IdentityException(IdentityHelper.GetIdentityResultErrorDescriptions(result));
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userIdOrEmail"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="IdentityException"></exception>
        public async Task<UserResponse> InitiateInstructorRoleAsync(string userIdOrEmail, CancellationToken token)
        {
            var user = await GetActiveUserByEmailAsync(userIdOrEmail, token);
            user.InstructorStatus = InstructorStatus.PendingApproval;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new IdentityException(IdentityHelper.GetIdentityResultErrorDescriptions(result));
            return user.Adapt<UserResponse>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="IdentityException"></exception>
        public async Task<(bool, UserResponse)> ApproveInstructorRequestAsync(InstructorApprovalCommand command, CancellationToken token)
        {
            var user = await GetActiveUserByEmailAsync(command.UserIdOrEmail, token);
            IdentityResult result = default!;

            if (user?.InstructorStatus != InstructorStatus.PendingApproval)
                throw new ForbiddenException(["Please send a request first"]);

            if (command.ApprovalStatus == InstructorStatus.Approved)
            {
                // assign role Instructor to user
                user.InstructorStatus = InstructorStatus.Approved;
                result = await _userManager.AddToRoleAsync(user, RoleConstants.Instructor);

                if (!result.Succeeded)
                    throw new IdentityException(IdentityHelper.GetIdentityResultErrorDescriptions(result));   
            }
            else
            {
                user.InstructorStatus = command.ApprovalStatus;
            }
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded) throw new IdentityException(IdentityHelper.GetIdentityResultErrorDescriptions(result));

            return (result.Succeeded, user.Adapt<UserResponse>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<UserResponse>> GetUserInstructorRequestsAsync(GetUserInstructorsQuery request, CancellationToken cancellationToken)
        {
            var userInstructors = await _userManager.Users.Where(u => u.InstructorStatus == InstructorStatus.PendingApproval).ToListAsync();
            return userInstructors.Adapt<List<UserResponse>>();
        }

        ///
        public async Task<UserResponse> UpdateUserAsync(CompleteUserProfileCommand command, CancellationToken token)
        {
            var user = await GetActiveUserByEmailAsync(command.Id.ToString(), token);
            var updatedUser = command.Adapt(user); // updating the extisting tracked user
            await _userManager.UpdateAsync(updatedUser);
            return updatedUser.Adapt<UserResponse>();
        }
    }
}
