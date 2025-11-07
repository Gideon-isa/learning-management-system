using Lms.Enrollment.Domain.Respositories;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using Mapster;

namespace Lms.Enrollment.Application.Features.CourseEnrollment.Queries
{
    public class GetAllAvailableStudentsQueryHandler : ICustomRequestHandler<GetAllAvailableStudentQuery, IResponseWrapper<List<AvailableStudentResponse>>>
    {
        private readonly IStudentRepository _studentRespository;

        public GetAllAvailableStudentsQueryHandler(IStudentRepository studentRespository)
        {
            _studentRespository = studentRespository;
        }

        public async Task<IResponseWrapper<List<AvailableStudentResponse>>> Handle(GetAllAvailableStudentQuery request, CancellationToken cancellationToken)
        {
            var students = await _studentRespository.GetAllStudents(cancellationToken);
            var studentDtos = students.Adapt<List<AvailableStudentResponse>>();
            return await ResponseWrapper<List<AvailableStudentResponse>>.SuccessAsync(data: studentDtos);
        }
    }
}
