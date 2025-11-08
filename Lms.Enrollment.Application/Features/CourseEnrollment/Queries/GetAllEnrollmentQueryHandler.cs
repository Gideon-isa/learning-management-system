using Lms.Enrollment.Domain.Respositories;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using Mapster;

namespace Lms.Enrollment.Application.Features.CourseEnrollment.Queries
{
    public class GetAllEnrollmentQueryHandler : ICustomRequestHandler<GetAllEnrollmentsQuery, IResponseWrapper<List<CourseEnrollmentResponse>>>
    {
        private readonly ICourseEnrollmentRespository _courseEnrollmentRespository;

        public GetAllEnrollmentQueryHandler(ICourseEnrollmentRespository courseEnrollmentRespository)
        {
            _courseEnrollmentRespository = courseEnrollmentRespository;
        }

        public async Task<IResponseWrapper<List<CourseEnrollmentResponse>>> Handle(GetAllEnrollmentsQuery request, CancellationToken cancellationToken)
        {
            var enrollments = await _courseEnrollmentRespository.GetAllAsync(cancellationToken);
            var enrollmentDto = enrollments.Adapt<List<CourseEnrollmentResponse>>();
            return await ResponseWrapper<List<CourseEnrollmentResponse>>.SuccessAsync(data: enrollmentDto, messages: [$"{enrollmentDto.Count} was retrieved"]);

        }
    }
}
