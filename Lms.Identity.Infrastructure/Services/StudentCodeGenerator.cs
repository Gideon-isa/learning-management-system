using Lms.Identity.Application.Abstractions;

namespace Lms.Identity.Infrastructure.Services
{
    public class StudentCodeGenerator : IStudentCodeGenerator
    {
        public string GenerateStudentCode()
        {
            return $"STU-{DateTime.UtcNow:yyyy}-{Guid.NewGuid().ToString("N")[..6].ToLower()}";
        }
    }
}
