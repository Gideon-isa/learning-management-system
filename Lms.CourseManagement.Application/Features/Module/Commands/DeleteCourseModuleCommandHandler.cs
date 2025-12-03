using Lms.CourseManagement.Application.Abstractions;
using Lms.CourseManagement.Domain.Exceptions;
using Lms.CourseManagement.Domain.Repositories;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using Lms.SharedKernel.Domain;

namespace Lms.CourseManagement.Application.Features.Module.Commands
{
    public class DeleteCourseModuleCommandHandler : ICustomRequestHandler<DeleteCourseModuleCommand, IResponseWrapper>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseManagementUnitOfWork _unitOfWork;

        public DeleteCourseModuleCommandHandler(ICourseRepository courseRepository, ICourseManagementUnitOfWork unitOfWork)
        {
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<IResponseWrapper> Handle(DeleteCourseModuleCommand request, CancellationToken cancellationToken)
        {
            var courseInDb = await _courseRepository.GetByIdAsync(request.CourseId, cancellationToken) 
                ?? throw DomainException.Create<CourseNotFoundException>("Course not found");
            courseInDb.RemoveModule(request.ModuleId);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return await ResponseWrapper.SuccessAsync("Course module deleted successfully.");
        }
    }
}
