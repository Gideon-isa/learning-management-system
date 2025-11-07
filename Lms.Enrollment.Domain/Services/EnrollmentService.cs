using Lms.Enrollment.Domain.Entities;
using Lms.Enrollment.Domain.Respositories;
using Lms.Enrollment.Domain.ValueObjects;
using System.Threading;

namespace Lms.Enrollment.Domain.Services
{
    public class EnrollmentService //: IDomainEnrollmentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseEnrollmentRespository _courseEnrollmentRespository;
        

        public EnrollmentService(IStudentRepository studentRepository, ICourseEnrollmentRespository courseEnrollmentRespository, 
            IAvailableCoursesRepository availableCoursesRepository)
        {
            _studentRepository = studentRepository;
            _courseEnrollmentRespository = courseEnrollmentRespository;
            
        }


        //public async Task<EnrollmentResult> EnrollStudents(CourseEnrollment courseEnrollment, IEnumerable<Guid> studentIds, Guid courseId, CancellationToken cancellationToken)
        //{
        //    //List<Guid> studentIdsList = [.. studentIds];
        //    //// Getting students
        //    //var students = await _studentRepository.GetStudentsByIds(studentIdsList, cancellationToken)
        //    //    ?? throw new Exception("All students are not yet eligible to be enrolled, kindly complete user's profile");

        //    //var courseEnrollment2 = await _courseEnrollmentRespository.GetCourseEnrollmentByCourseId(courseId, cancellationToken);

        //    //if (courseEnrollment == null)
        //    //{
        //    //    courseEnrollment = Domain.Entities.CourseEnrollment.Create(courseId);
        //    //    _ = await _courseEnrollmentRespository.CreateCourseEnrollment(courseEnrollment, cancellationToken);
        //    //}

        //    // comparing student count
        //    //if (students.Count != studentIdsList.Count)
        //    //    throw new Exception("Some students were not found or are not eligible for enrollment.");

        //    //// Getting the available course
        //    //var availableCourse = await _availableCoursesRepository.GetAvailableCourseByIdAsync(courseId, cancellationToken)
        //    //    ?? throw new Exception("Course not found");

        //    //var enrollmentResult = courseEnrollment.EnrollStudent(students);
        //    //return enrollmentResult;
        //}
    }
}
