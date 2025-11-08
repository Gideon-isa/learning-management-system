using Lms.Enrollment.Application.Abstractions;
using Lms.Enrollment.Domain.Respositories;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.Enrollment.Application.Features.CourseEnrollment.Command
{
    public class UnEnrollStudentCommandHandler : ICustomRequestHandler<UnEnrollStudentCommand, IResponseWrapper>
    {
        private readonly IEnrollmentUnitOfWork _unitOfWork;
        private readonly ICourseEnrollmentRespository _courseEnrollmentRespository;
        private readonly IAvailableCoursesRepository _availableCoursesRepository;
        private readonly IStudentRepository _studentRepository;

        public UnEnrollStudentCommandHandler(
            IStudentRepository studentRepository, 
            IAvailableCoursesRepository availableCoursesRepository, 
            ICourseEnrollmentRespository courseEnrollmentRespository, 
            IEnrollmentUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _availableCoursesRepository = availableCoursesRepository;
            _courseEnrollmentRespository = courseEnrollmentRespository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResponseWrapper> Handle(UnEnrollStudentCommand request, CancellationToken cancellationToken)
        {
            var courseEnrollment = await _courseEnrollmentRespository.GetCourseEnrollmentById(request.EnrollmentId, cancellationToken)
                ?? throw new Exception("Enrollment not found");

            var student = await _studentRepository.GetStudentByIdAsync(request.StudentId, cancellationToken)
                ?? throw new Exception("Student are not enrolled");

            courseEnrollment.WithdrawStudent(student.Id);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return await ResponseWrapper.SuccessAsync([$"{student.FirstName} has been successfully unenrolled"]);
        }
    }
}
