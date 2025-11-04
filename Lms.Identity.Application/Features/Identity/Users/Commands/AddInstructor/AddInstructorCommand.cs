using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.Identity.Application.Features.Identity.Users.Commands.AddInstructor
{
    public class AddInstructorCommand : ICustomRequest<IResponseWrapper>
    {
        public string UserIdOrEmail { get; set; }
    }
}
