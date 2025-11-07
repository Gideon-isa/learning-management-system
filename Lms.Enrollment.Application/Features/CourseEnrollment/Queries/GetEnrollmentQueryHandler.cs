using Lms.Enrollment.Domain.Respositories;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using Mapster;

namespace Lms.Enrollment.Application.Features.CourseEnrollment.Queries
{
    public class GetEnrollmentQueryHandler : ICustomRequestHandler<GetEnrollmentByIdQuery, IResponseWrapper<CourseEnrollmentResponse>>
    {
        private readonly ICourseEnrollmentRespository _courseEnrollmentRespository;

        public GetEnrollmentQueryHandler(ICourseEnrollmentRespository courseEnrollmentRespository)
        {
            _courseEnrollmentRespository = courseEnrollmentRespository;
        }

        public async Task<IResponseWrapper<CourseEnrollmentResponse>> Handle(GetEnrollmentByIdQuery request, CancellationToken cancellationToken)
        {
            var enrollment = await _courseEnrollmentRespository.GetCourseEnrollmentById(request.Id, cancellationToken) 
                ?? throw new Exception("not found");

            var enrollmentDto = enrollment.Adapt<CourseEnrollmentResponse>();
            return await ResponseWrapper<CourseEnrollmentResponse>.SuccessAsync(enrollmentDto);
        }
    }
}
