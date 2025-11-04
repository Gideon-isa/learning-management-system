using Lms.CourseManagement.Application.Abstractions;
using Lms.CourseManagement.Application.Features.Course.Commands.PublishCourse;
using Lms.CourseManagement.Domain.Repositories;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using Lms.SharedKernel.Interfaces;

namespace Lms.CourseManagement.Application.Features.CourseFeatures.Commands.PublishCourse
{
    public class PublishCourseCommandHandler : ICustomRequestHandler<PublishCourseCommand, IResponseWrapper>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseManagementUnitOfWork _unitOfWork;

        public PublishCourseCommandHandler(ICourseRepository courseRepository, ICourseManagementUnitOfWork unitOfWork)
        {
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResponseWrapper> Handle(PublishCourseCommand command, CancellationToken cancellationToken)
        {
            var courseInDb = await _courseRepository.GetByIdAsync(command.CourseId, cancellationToken);

            if (courseInDb is null)
            {
                // throw a domain custom exception instead
                throw new Exception(); 
            }

            courseInDb.Publish();

            await _unitOfWork.SaveChangesAsync();

            return ResponseWrapper.Success("Course published");
        }
    }
}
