using Lms.SharedKernel.Domain;

namespace Lms.ContentDelivery.Domain.Entities
{
    public class StudentAccess : AggregateRoot<Guid>
    {
        public string StudentCode { get; private set; }
        public Guid CourseId { get; private set; }
        public DateTime GrantedAt { get; private set; }
        public bool IsRevoked { get; private set; }
        public DateTime? RevokedAt { get; private set; }


        private StudentAccess() { } // EF Core

        private StudentAccess(string studentCode, Guid courseId)
        {
            StudentCode = studentCode;
            CourseId = courseId;
            GrantedAt = DateTime.UtcNow;
            IsRevoked = false;
        }

        public static StudentAccess Create(string studentCode, Guid courseId)
        {
            if (string.IsNullOrEmpty(studentCode))
                throw new Exception("Student Code can not be empty"); // TODO: Use Custom Error
         
            return new StudentAccess(studentCode, courseId);
        }


        public void GrantAccess()
        {
            if (IsRevoked)
            {
                IsRevoked = false;
                RevokedAt = null;
                GrantedAt = DateTime.UtcNow;
            }
        }

        public void RevokeAccess() 
        {
            if (!IsRevoked)
            { 
                IsRevoked = true;
                RevokedAt = DateTime.UtcNow;
            }
        }
    }
}
