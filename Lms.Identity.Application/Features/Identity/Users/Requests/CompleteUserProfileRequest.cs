using Lms.SharedKernel.Domain.Enums;

namespace Lms.Identity.Application.Features.Identity.Users.Requests
{
    public class CompleteUserProfileRequest
    {
        public string? FirstName { get; set; } = default!;
        public string? LastName { get; set; } = default!;
        public string? MiddleName { get; set; }
        public string? DisplayName { get; set; }
        public string? PhoneNumber { get; set; }

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
        public string? StaffId { get; set; }

        // Audit & Preferences
        public string? PreferredLanguage { get; set; } // e.g., "en", "fr"
    }
}
