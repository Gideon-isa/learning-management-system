using Lms.SharedKernel.Domain;

namespace Lms.CourseManagement.Domain.Entities
{
    public class Instructor : AggregateRoot<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImageUrl { get; set; }
        public string DisplayName { get; set; }
        public string Bio { get; set; }
        public string Department { get; set; }
        public string Institution { get; set; }
        public string StaffId { get; set; }

        public Instructor() {} // EF Core

        private Instructor(Guid id, string firstName, string lastName, string profileImageUrl, string displayName, string bio, string department, string institution, string staffId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            ProfileImageUrl = profileImageUrl;
            DisplayName = displayName;
            Bio = bio;
            Department = department;
            Institution = institution;
            StaffId = staffId;      
        }

        public static Instructor Create(
            Guid id, 
            string firstName, 
            string lastName, 
            string profileImageUrl, 
            string displayName, 
            string bio, 
            string department, 
            string institution, 
            string staffId)

            => new(id, firstName, lastName, profileImageUrl, displayName, bio, department, institution, staffId);

    }
}
