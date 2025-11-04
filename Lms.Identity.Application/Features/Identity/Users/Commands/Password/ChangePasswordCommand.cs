using Lms.Identity.Application.Features.Identity.Users.Requests;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using MediatR;

namespace Lms.Identity.Application.Features.Identity.Users.Commands.Password
{
    public class ChangePasswordCommand : ICustomRequest<IResponseWrapper>
    {
        public ChangePasswordRequest Request { get; set; }

    }
}
