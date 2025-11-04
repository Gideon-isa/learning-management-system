namespace Lms.Identity.Application.Features.Identity.Users
{
    public class UserResponse
    {
        // Basic Information
        public string Id { get; set; }
        public string? FirstName { get; set; } = default!;
        public string? LastName { get; set; } = default!;
        public string? MiddleName { get; set; }
        public string? DisplayName { get; set; }
        public string? Email { get; set; } = string.Empty;

        // Profile and Contact Information
        public string? ProfileImageUrl { get; set; }
        public string? Bio { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public DateTime? DateOfBirth { get; set; }

        // academic and professional details
        public string? Department { get; set; } // e.g. for instructors
        public string? Institution { get; set; }
        public string? EnrollmentNumber { get; set; } // for students
        public string? StaffId { get; set; }

        public string InstructorStatus { get; set; } = string.Empty;

        // System and Status Information
        public bool IsActive { get; set; } = true;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? LastLogin { get; set; }
        public string? CreatedBy { get; set; }

        // Audit & Preferences
        public string? PreferredLanguage { get; set; } // e.g., "en", "fr"
        public string? TimeZone { get; set; }
        public bool IsEmailVerified { get; set; } = false;
    }

    public class UserResponses
    {
        public List<UserResponse> responses { get; set; } = [];

    }
}
