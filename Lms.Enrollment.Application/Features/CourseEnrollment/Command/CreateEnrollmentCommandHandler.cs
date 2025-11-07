using Lms.Enrollment.Application.Abstractions;
using Lms.Enrollment.Domain.Entities;
using Lms.Enrollment.Domain.Respositories;
using Lms.Enrollment.Domain.Services;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using Mapster;

namespace Lms.Enrollment.Application.Features.CourseEnrollment.Command
{
    public class CreateEnrollmentCommandHandler : ICustomRequestHandler<CreateEnrollmentCommand, IResponseWrapper<CourseEnrollmentResponse>>
    {
        private readonly IEnrollmentUnitOfWork _unitOfWork;
        private readonly ICourseEnrollmentRespository _courseEnrollmentRespository;
        private readonly IAvailableCoursesRepository _availableCoursesRepository;
        private readonly IStudentRepository _studentRepository;

        public CreateEnrollmentCommandHandler(
            ICourseEnrollmentRespository courseEnrollmentRespository, 
            IEnrollmentUnitOfWork unitOfWork, 
            IAvailableCoursesRepository availableCoursesRepository,
            IStudentRepository studentRepository)
        {
            _unitOfWork = unitOfWork;
            _courseEnrollmentRespository = courseEnrollmentRespository;
            _availableCoursesRepository = availableCoursesRepository;
            _studentRepository = studentRepository;
        }

        public async Task<IResponseWrapper<CourseEnrollmentResponse>> Handle(CreateEnrollmentCommand request, CancellationToken cancellationToken)
        {
            // Getting the available course
            var availableCourse = await _availableCoursesRepository.GetAvailableCourseByIdAsync(request.CourseId, cancellationToken)
                ?? throw new Exception("Course is not available for enrollment");

            List<Guid> studentIdsList = [.. request.StudentIds];
            // Getting students
            var students = await _studentRepository.GetStudentsByIds(studentIdsList, cancellationToken)
                ?? throw new Exception("Student not yet eligible to be enrolled, kindly complete user's profile");

            // retrieving the course enrollment and if non exists, creating and instance
            var courseEnrollment = await _courseEnrollmentRespository.GetCourseEnrollmentByCourseId(request.CourseId, cancellationToken);

            if (courseEnrollment is null)
            {
                courseEnrollment = Domain.Entities.CourseEnrollment.Create(availableCourse.CourseId, availableCourse.CourseTitle);
                _ = await _courseEnrollmentRespository.CreateCourseEnrollment(courseEnrollment, cancellationToken);
            }

            var enrollmentResult = courseEnrollment.EnrollStudent(students);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var courseEnrollmentDto = courseEnrollment.Adapt<CourseEnrollmentResponse>();

            return await ResponseWrapper<CourseEnrollmentResponse>
                .SuccessAsync(data: courseEnrollmentDto, messages: [$"{enrollmentResult.Enrolled.Count} students were enrolled and {enrollmentResult.Skipped.Count} were already enrolled"]);
        }
    }
}
