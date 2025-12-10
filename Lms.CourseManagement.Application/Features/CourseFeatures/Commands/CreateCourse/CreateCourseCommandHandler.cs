using Lms.CourseManagement.Application.Abstractions;
using Lms.CourseManagement.Application.Features.Course.DTO;
using Lms.CourseManagement.Domain.Exceptions;
using Lms.CourseManagement.Domain.Repositories;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using Lms.SharedKernel.Domain;
using Mapster;

namespace Lms.CourseManagement.Application.Features.CourseFeatures.Commands.CreateCourse
{
    public class CreateCourseCommandHandler : ICustomRequestHandler<CreateCourseCommand, IResponseWrapper<CourseResponse>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseCategoryRepository _courseCategoryRepository;
        private readonly ICourseManagementUnitOfWork _unitOfWork;


        public CreateCourseCommandHandler(ICourseRepository courseRepository, ICourseManagementUnitOfWork unitOfWork, ICourseCategoryRepository courseCategoryRepository)
        {
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
            _courseCategoryRepository = courseCategoryRepository;
        }

        public async Task<IResponseWrapper<CourseResponse>> Handle(CreateCourseCommand command, CancellationToken cancellationToken)
        {
            var courseCategory = await _courseCategoryRepository.GetAsync(command.CategoryId, cancellationToken) 
                ?? throw DomainException.Create<CourseCategoryNotFoundException>($"Course category with id {command.CategoryId} not found.");

            var newCourse = Domain.Entities.Course.Create(command.CourseTitle, command.CourseCode, command.Description, command.CategoryId, command.InstructorId);
            await _courseRepository.AddAsync(newCourse, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var courseResponse = newCourse.Adapt<CourseResponse>(); 
            return await ResponseWrapper<CourseResponse>.SuccessAsync(data: courseResponse, ["Course created successfully"]);
        }
    }
}
