using Lms.CourseManagement.Application.Abstractions;
using Lms.CourseManagement.Application.Features.Course.DTO;
using Lms.CourseManagement.Domain.Repositories;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using Mapster;

namespace Lms.CourseManagement.Application.Features.CourseFeatures.Commands.CreateCourse
{
    public class CreateCourseCommandHandler : ICustomRequestHandler<CreateCourseCommand, IResponseWrapper<CourseResponse>>
    {
        private readonly ICourseRepository _courseRepository;
        
        private readonly ICourseManagementUnitOfWork _unitOfWork;


        public CreateCourseCommandHandler(ICourseRepository courseRepository, ICourseManagementUnitOfWork unitOfWork)
        {
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork; 
        }

        public async Task<IResponseWrapper<CourseResponse>> Handle(CreateCourseCommand command, CancellationToken cancellationToken)
        {
            
            var newCourse = Domain.Entities.Course.Create(command.CourseTitle, command.CourseCode, command.Description, command.Category, command.InstructorId);
            await _courseRepository.AddAsync(newCourse, cancellationToken);
            await _unitOfWork.SaveChangesAsync();
            var courseResponse = newCourse.Adapt<CourseResponse>(); 
            return await ResponseWrapper<CourseResponse>.SuccessAsync(data: courseResponse, ["Course created successfully"]);
        }
    }
}
