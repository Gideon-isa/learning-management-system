using Lms.CourseManagement.Domain.Exceptions;
using Lms.CourseManagement.Domain.Repositories;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using Lms.SharedKernel.Domain;
using Mapster;

namespace Lms.CourseManagement.Application.Features.CourseFeatures.Queries.CourseModule
{
    public class GetCourseModulesQueryHandler : ICustomRequestHandler<GetCourseModulesQuery, IResponseWrapper<CourseModuleDto>>
    {
        private readonly ICourseRepository _courseRepository;

        public GetCourseModulesQueryHandler(ICourseRepository courseModuleRepository)
        {
            _courseRepository = courseModuleRepository;
        }

        public async Task<IResponseWrapper<CourseModuleDto>> Handle(GetCourseModulesQuery request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.CourseId, cancellationToken) ??
                throw DomainException.Create<CourseNotFoundException>($"Course with Id {request.CourseId} was not found.");
            var courseModuleDto = course.Adapt<CourseModuleDto>();
            return await ResponseWrapper<CourseModuleDto>.SuccessAsync(data: courseModuleDto, messages: ["Course modules retrieved successfully"]);
        }
    }
}
