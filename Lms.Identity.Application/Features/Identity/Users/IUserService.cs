using Lms.Identity.Application.Features.Identity.Users.Commands.AddUser;
using Lms.Identity.Application.Features.Identity.Users.Commands.CompleteUserProfile;
using Lms.Identity.Application.Features.Identity.Users.Commands.InstructorApproval;
using Lms.Identity.Application.Features.Identity.Users.Commands.Password;
using Lms.Identity.Application.Features.Identity.Users.Queries.GetAll;
using Lms.Identity.Application.Features.Identity.Users.Queries.GetAllUserInstructor;
using Lms.Identity.Application.Features.Identity.Users.Queries.GetUser;

namespace Lms.Identity.Application.Features.Identity.Users
{
    public interface IUserService
    {
        Task<UserResponse> CreateAsync(RegisterUserCommand command, CancellationToken token);
        Task<UserResponse> GetUserAsync(GetUserQuery query, CancellationToken token);

        Task<List<UserResponse>> GetAllAsync(GetAllUsersQuery query, CancellationToken token);
        Task<List<UserResponse>> GetAllActiveAsync(GetAllActiveUsersQuery query, CancellationToken token);
        //Task<UserResponse> GetByEmailAsync(GetUserByEmailQuery query, CancellationToken token);
        Task<string> ChangePasswordAsync(ChangePasswordCommand command, CancellationToken token);

        Task<string> ForgotPasswordAsync(ForgotPasswordCommand request, CancellationToken token);

        Task<string> ResetPasswordAsync(ResetPasswordCommand command, CancellationToken token);

        Task<string> DeleteAsync(string id, CancellationToken token);
        Task<UserResponse> InitiateInstructorRoleAsync(string userIdOrEmail, CancellationToken token);
        Task<(bool, UserResponse)> ApproveInstructorRequestAsync(InstructorApprovalCommand command, CancellationToken token);
        Task<List<UserResponse>> GetUserInstructorRequestsAsync(GetUserInstructorsQuery request, CancellationToken token);
        Task<UserResponse> UpdateUserAsync(CompleteUserProfileCommand command, CancellationToken token);
        Task<UserResponse> CompleteProfileAsync(CompleteUserProfileCommand command, CancellationToken token);
    }
}
