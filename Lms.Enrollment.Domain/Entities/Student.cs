using Lms.SharedKernel.Domain;

namespace Lms.Enrollment.Domain.Entities
{
    public class Student : AggregateRoot<Guid>
    {
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string? Username { get; private set; }
        public string StudentCode { get; private set; }
        public bool IsActive { get; private set; } = true;
        
        private Student() { } // EF Core
      
        private Student(Guid id, string firstName, string lastName, string username, string studentCode) 
        { 
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            StudentCode = studentCode;  
            IsActive = true;
        }

        public static Student Create(Guid id, string firstName, string lastName, string username, string studentCode)
        {
            return new Student(id, firstName, lastName, username, studentCode);
        }
    }
}
