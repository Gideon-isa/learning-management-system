using Lms.CourseManagement.Application.Features.Course.DTO;
using Lms.CourseManagement.Domain.Repositories;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using Mapster;

namespace Lms.CourseManagement.Application.Features.Course.Queries.GetAllCourse
{
    public class GetCoursesQueryHandler : ICustomRequestHandler<GetCoursesQuery, IResponseWrapper<CoursesResponse>>
    {
        private ICourseRepository _courseRepository;

        public GetCoursesQueryHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IResponseWrapper<CoursesResponse>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = await _courseRepository.GetAllAsync(cancellationToken);
            var courseResponse = courses.Adapt<List<CourseResponse>>();
            var coursesDto = new CoursesResponse { courseResponses = courseResponse };
            return await ResponseWrapper<CoursesResponse>.SuccessAsync(data: coursesDto, [$"{courseResponse.Count} courses where retreived"]);
        }
    }
}
