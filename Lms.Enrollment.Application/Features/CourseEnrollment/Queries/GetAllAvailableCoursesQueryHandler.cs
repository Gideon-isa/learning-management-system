using Lms.Enrollment.Domain.Respositories;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using Mapster;

namespace Lms.Enrollment.Application.Features.CourseEnrollment.Queries
{
    public class GetAllAvailableCoursesQueryHandler : ICustomRequestHandler<GetAllAvailableCoursesQuery, IResponseWrapper<List<CourseEnrollmentResponse>>>
    {
        private readonly IAvailableCoursesRepository _availableCoursesRepository;

        public GetAllAvailableCoursesQueryHandler(IAvailableCoursesRepository availableCoursesRepository)
        {
            _availableCoursesRepository = availableCoursesRepository;
        }

        public async Task<IResponseWrapper<List<CourseEnrollmentResponse>>> Handle(GetAllAvailableCoursesQuery request, CancellationToken cancellationToken)
        {
            var availableCourses = await _availableCoursesRepository.GetAvailableCoursesAsync(cancellationToken);
            var availableCourseDto = availableCourses.Adapt<List<CourseEnrollmentResponse>>();
            return await ResponseWrapper<List<CourseEnrollmentResponse>>.SuccessAsync(data: availableCourseDto);
        }
    }
}
