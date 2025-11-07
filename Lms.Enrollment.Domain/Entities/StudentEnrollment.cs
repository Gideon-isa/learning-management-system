using Lms.Enrollment.Domain.Enums;
using Lms.SharedKernel.Domain;

namespace Lms.Enrollment.Domain.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class StudentEnrollment
    {
        public Guid StudentId { get; private set; }
        public string StudentName { get; private set; }
        public DateTime EnrolledOn { get; private set; }
        public EnrollmentStatus Status { get; private set; }

        private StudentEnrollment() { } // EF Core

        private StudentEnrollment(Guid studentId, string studentName)
        {
            StudentId = studentId;
            StudentName = studentName;
            EnrolledOn = DateTime.UtcNow;
            Status = EnrollmentStatus.Active;
        }

        internal static StudentEnrollment Create(Guid studentId, string studentName)
            => new StudentEnrollment(studentId, studentName);

        public void Withdraw() => Status = EnrollmentStatus.Withdrawn;

    }   
}
