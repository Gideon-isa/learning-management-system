using Lms.CourseManagement.Application.Abstractions;
using Lms.CourseManagement.Application.Features.Course.DTO;
using Lms.CourseManagement.Application.Features.Course.Queries.GetAllCourse;
using Lms.CourseManagement.Application.Features.CourseFeatures.Sorting;
using Lms.CourseManagement.Domain.Filters;
using Lms.CourseManagement.Domain.Repositories;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using Mapster;

namespace Lms.CourseManagement.Application.Features.CourseFeatures.Queries.GetAllCourse
{
    public class GetCoursesQueryHandler : ICustomRequestHandler<GetCoursesQuery, IResponseWrapper<CoursesResponse>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly SortMappingProvider _mappingProvider;

        public GetCoursesQueryHandler(ICourseRepository courseRepository, SortMappingProvider mappingProvider)
        {
            _courseRepository = courseRepository;
            _mappingProvider = mappingProvider;
        }

        public async Task<IResponseWrapper<CoursesResponse>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
        {
            CourseFilter courseFilter = request.Adapt<CourseFilter>();
            var isValid = _mappingProvider.ValidateMappings<GetCoursesQuery, Domain.Entities.Course>(courseFilter.Sort);
            if (!isValid)
                throw new Exception("The provided sort parameter isn't valid");          
            
            var courses = await _courseRepository.GetAllAsync(courseFilter, cancellationToken);
            var courseResponse = courses.Adapt<List<CourseResponse>>();
            var coursesDto = new CoursesResponse { courseResponses = courseResponse };
            return await ResponseWrapper<CoursesResponse>.SuccessAsync(data: coursesDto, [$"{courseResponse.Count} courses where retreived"]);
        }
    }
}
