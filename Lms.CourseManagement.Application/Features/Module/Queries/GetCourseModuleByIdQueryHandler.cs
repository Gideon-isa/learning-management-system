using Lms.CourseManagement.Application.Features.Module.Dto;
using Lms.CourseManagement.Domain.Repositories;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using Mapster;

namespace Lms.CourseManagement.Application.Features.Module.Queries
{
    public class GetCourseModuleByIdQueryHandler : ICustomRequestHandler<GetCourseModuleByIdQuery, IResponseWrapper<CourseModuleResponse>>
    {
        private readonly ICourseRepository _courseRepository;

        public GetCourseModuleByIdQueryHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<IResponseWrapper<CourseModuleResponse>> Handle(GetCourseModuleByIdQuery request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.CourseId, cancellationToken);
            if (course == null)
            {
                throw new Exception("Course not found");
            }
            var module = course.GetModuleById(request.ModuleId);
            var moduleDto = module.Adapt<CourseModuleResponse>();

            return ResponseWrapper<CourseModuleResponse>.Success(moduleDto);
        }
    }
}
