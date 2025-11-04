using Lms.Identity.Application.Features.Identity.Users.Requests;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using MediatR;

namespace Lms.Identity.Application.Features.Identity.Users.Commands.Password
{
    public class ResetPasswordCommand : ICustomRequest<IResponseWrapper>
    {
        public ResetPasswordRequest Request { get; set; }
    }
}
