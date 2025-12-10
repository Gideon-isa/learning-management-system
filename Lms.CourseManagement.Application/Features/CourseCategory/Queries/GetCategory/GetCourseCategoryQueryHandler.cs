using Lms.CourseManagement.Application.Features.CourseCategory.DTO;
using Lms.CourseManagement.Domain.Exceptions;
using Lms.CourseManagement.Domain.Repositories;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using Lms.SharedKernel.Domain;
using Mapster;

namespace Lms.CourseManagement.Application.Features.CourseCategory.Queries.GetCategory
{
    public class GetCourseCategoryQueryHandler : ICustomRequestHandler<GetCourseCategoryQuery, IResponseWrapper<CourseCategoryDto>>
    {
        private readonly ICourseCategoryRepository _courseCategoryRepository;
        public GetCourseCategoryQueryHandler(ICourseCategoryRepository courseCategoryRepository)
        {
            _courseCategoryRepository = courseCategoryRepository;
        }
        public async Task<IResponseWrapper<CourseCategoryDto>> Handle(GetCourseCategoryQuery request, CancellationToken cancellationToken)
        {
            var courseCategory = await _courseCategoryRepository.GetAsync(request.Id, cancellationToken) 
                ?? throw DomainException.Create<CourseCategoryNotFoundException>("Course Category not Found");

            var courseCategoryDto = courseCategory.Adapt<CourseCategoryDto>();
            return await ResponseWrapper<CourseCategoryDto>.SuccessAsync(courseCategoryDto, ["retrieved successfully"]);
        }
    }
}

