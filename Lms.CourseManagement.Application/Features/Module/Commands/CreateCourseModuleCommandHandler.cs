using Lms.CourseManagement.Application.Abstractions;
using Lms.CourseManagement.Application.Features.Module.Dto;
using Lms.CourseManagement.Domain.Entities;
using Lms.CourseManagement.Domain.Repositories;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using Mapster;

namespace Lms.CourseManagement.Application.Features.Module.Commands
{
    public class CreateCourseModuleCommandHandler : ICustomRequestHandler<CreateCourseModuleCommand, IResponseWrapper<CourseModuleResponse>>
    {
        private readonly ICourseRepository    _courseRepository;
        private readonly ICourseManagementUnitOfWork _unitOfWork;

        public CreateCourseModuleCommandHandler(ICourseRepository courseRepository, ICourseManagementUnitOfWork unitOfWork)
        {
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResponseWrapper<CourseModuleResponse>> Handle(CreateCourseModuleCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.CourseId, cancellationToken);
            if (course == null)
            {
                // TODO: use a custom exception here
                throw new Exception("Course not found");
            }
            var newCourseModule = CourseModule.Create(request.Title, request.Description);
            course.AddModuleToCourse(newCourseModule);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var response = newCourseModule.Adapt<CourseModuleResponse>();
            return await ResponseWrapper<CourseModuleResponse>.SuccessAsync(data: response, ["Course module created successfully"]);
        }
    }
}
