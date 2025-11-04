using Lms.Identity.Application.Abstractions;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using Lms.SharedKernel.Interfaces;
using MediatR;

namespace Lms.Identity.Application.Features.Identity.Users.Commands.Password
{
    public class ChangePasswordCommandHandler : ICustomRequestHandler<ChangePasswordCommand, IResponseWrapper>
    {
        private readonly IUserService _userService;
        private readonly IIdentityUnitOfWork _unitOfWork;

        public ChangePasswordCommandHandler(IUserService userService, IIdentityUnitOfWork unitOfWork)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResponseWrapper> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var response = await _userService.ChangePasswordAsync(request, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return await ResponseWrapper.SuccessAsync(response);
        }
    }
}
