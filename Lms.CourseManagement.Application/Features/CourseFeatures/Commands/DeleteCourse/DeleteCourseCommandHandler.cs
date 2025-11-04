using Lms.CourseManagement.Application.Abstractions;
using Lms.CourseManagement.Domain.Repositories;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.CourseManagement.Application.Features.CourseFeatures.Commands.DeleteCourse
{
    public class DeleteCourseCommandHandler : ICustomRequestHandler<DeleteCourseCommand, IResponseWrapper>
    {
        private readonly ICourseRepository _courseRepository;

        public DeleteCourseCommandHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IResponseWrapper> Handle(DeleteCourseCommand command, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(command.CourseId, cancellationToken);
            if ( course is null) 
            {
                // TODO Define Custom Exceptions
                throw new Exception();
            }
            course.MarkAsDeleted();
            await _courseRepository.UpdateAsync(course, cancellationToken);
            return await ResponseWrapper.SuccessAsync("Deleted Successfully");
        }
    }
}
