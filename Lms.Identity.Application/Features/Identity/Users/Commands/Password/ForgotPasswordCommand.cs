using Lms.Identity.Application.Features.Identity.Users.Requests;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using MediatR;

namespace Lms.Identity.Application.Features.Identity.Users.Commands.Password
{
    public class ForgotPasswordCommand : ICustomRequest<IResponseWrapper>
    {
        public ForgotPasswordRequest Request { get; set; }
    }
}
