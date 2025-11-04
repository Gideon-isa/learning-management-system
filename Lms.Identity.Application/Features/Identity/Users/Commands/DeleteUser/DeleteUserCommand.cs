using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using MediatR;

namespace Lms.Identity.Application.Features.Identity.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : ICustomRequest<IResponseWrapper>
    {
        public string UserId { get; set; }
    }
}
