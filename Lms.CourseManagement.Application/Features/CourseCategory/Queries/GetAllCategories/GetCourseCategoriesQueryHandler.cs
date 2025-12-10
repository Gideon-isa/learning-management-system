using Lms.CourseManagement.Application.Features.CourseCategory.DTO;
using Lms.CourseManagement.Domain.Repositories;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using Mapster;

namespace Lms.CourseManagement.Application.Features.CourseCategory.Queries.GetAllCategories
{
    public class GetCourseCategoriesQueryHandler : ICustomRequestHandler<GetCourseCategoriesQuery, IResponseWrapper<List<CourseCategoryDto>>>
    {
        private readonly ICourseCategoryRepository _courseCategoryRepository;
        public GetCourseCategoriesQueryHandler(ICourseCategoryRepository courseCategoryRepository)
        {
            _courseCategoryRepository = courseCategoryRepository;
        }
        public async Task<IResponseWrapper<List<CourseCategoryDto>>> Handle(GetCourseCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _courseCategoryRepository.GetAllAsync(cancellationToken);
            var categoryDtos = categories.Adapt<List<CourseCategoryDto>>();
            return await ResponseWrapper<List<CourseCategoryDto>>.SuccessAsync(data: categoryDtos, ["Course categories retrieved successfully"]);
        }
    }
}
