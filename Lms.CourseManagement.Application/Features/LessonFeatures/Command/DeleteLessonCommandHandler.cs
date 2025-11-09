using Lms.CourseManagement.Application.Abstractions;
using Lms.CourseManagement.Domain.Repositories;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.CourseManagement.Application.Features.LessonFeatures.Command
{
    public class DeleteLessonCommandHandler : ICustomRequestHandler<DeleteLessonCommand, IResponseWrapper>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseManagementUnitOfWork _unitOfWork;

        public DeleteLessonCommandHandler(ICourseManagementUnitOfWork unitOfWork, ICourseRepository courseRepository)
        {
            _unitOfWork = unitOfWork;
            _courseRepository = courseRepository;
        }

        public Task<IResponseWrapper> Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
