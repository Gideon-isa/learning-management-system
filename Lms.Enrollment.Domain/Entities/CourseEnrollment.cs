using Lms.Enrollment.Domain.Enums;
using Lms.Enrollment.Domain.Events;
using Lms.Enrollment.Domain.ValueObjects;
using Lms.SharedKernel.Domain;

namespace Lms.Enrollment.Domain.Entities
{
    public class CourseEnrollment : AggregateRoot<Guid>
    {
        private readonly List<StudentEnrollment> _studentEnrollments = new();
        public IReadOnlyCollection<StudentEnrollment> StudentEnrollments => _studentEnrollments.AsReadOnly(); 
        public Guid CourseId { get; private set; }
        public string CourseTitle { get; private set; }

        
        private CourseEnrollment() { } // EF Core

        private CourseEnrollment (Guid courseId, string courseTitle)
        {
            CourseId = courseId;
            CourseTitle = courseTitle;
        }

        public static CourseEnrollment Create(Guid courseId, string courseTitle) => new CourseEnrollment (courseId, courseTitle);

        public EnrollmentResult EnrollStudent(IEnumerable<Student> students)
        {
            //if (_studentEnrollments.Any(se => se.StudentId == student.Id))
            //    throw new InvalidOperationException("student already enrolled in this course");

            // checking for duplicate ids
            //var studentIds = _studentEnrollments.Select((st, index) => st.StudentId).ToList();

            var enrolled = new List<StudentSummary>();
            var skipped = new List<StudentSummary>();

            foreach (var student in students)
            { 
                if (_studentEnrollments.Any(se => se.StudentId == student.Id))
                {
                    skipped.Add(new StudentSummary(student.Id, student.FirstName));
                    continue;
                }
                _studentEnrollments.Add(StudentEnrollment.Create(student.Id, student.FirstName));
                enrolled.Add(new StudentSummary(student.Id, student.FirstName));

                // adding to the Domain Event
                AddDomainEvent(new EnrollStudentEvent(student.StudentCode, CourseId));
            }
            return new EnrollmentResult(enrolled, skipped);
        }

        public void WithdrawStudent(Guid studentId)
        {
            var studentEnrollment = _studentEnrollments.FirstOrDefault(e => e.StudentId == studentId);
            if (studentEnrollment is null)
                throw new InvalidOperationException("Student not enrolled in this course.");

            studentEnrollment.Withdraw();
            _studentEnrollments.Remove(studentEnrollment);
        }

        //public void Publish(List<Student> students)
        //{
        //    var enrollEvents = students.Select(s => new EnrollStudentEvent(s.StudentCode, CourseId));
        //    foreach (var enrollEvent in enrollEvents)
        //    { 
        //        AddDomainEvent(enrollEvent);   
        //    }
        //}
    }
}
