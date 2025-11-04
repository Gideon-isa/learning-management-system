using Lms.Identity.Application.Features.Identity.Users.Requests;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using MediatR;

namespace Lms.Identity.Application.Features.Identity.Users.Commands.AddUser
{
    public class RegisterUserCommand : ICustomRequest<IResponseWrapper>
    {
        public RegisterUserRequest? RegisterUserRequest { get; set; }
    }
}
