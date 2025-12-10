using Lms.CourseManagement.Application.Abstractions;
using Lms.CourseManagement.Domain.Entities;
using Lms.CourseManagement.Domain.Repositories;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using Lms.SharedKernel.Domain;
using Mapster;

namespace Lms.CourseManagement.Application.Features.CourseCategory.Command.CreateCategory
{
    public class CreateCourseCategoryCommandHandler : ICustomRequestHandler<CreateCourseCategoryCommand, IResponseWrapper<Guid>>
    {
        private readonly ICourseCategoryRepository _courseCategoryRepository;
        private readonly ICourseManagementUnitOfWork _unitOfWork;
        public CreateCourseCategoryCommandHandler(ICourseCategoryRepository courseCategoryRepository, ICourseManagementUnitOfWork unitOfWork)
        {
            _courseCategoryRepository = courseCategoryRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<IResponseWrapper<Guid>> Handle(CreateCourseCategoryCommand request, CancellationToken cancellationToken)
        {
            var newCourseCategory = Domain.Entities.CourseCategory.Create(request.Name);
            await _courseCategoryRepository.AddAsync(newCourseCategory, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return await ResponseWrapper<Guid>.SuccessAsync(data: newCourseCategory.Id, messages:["Course category created successfully."]);
        }
    }
}
