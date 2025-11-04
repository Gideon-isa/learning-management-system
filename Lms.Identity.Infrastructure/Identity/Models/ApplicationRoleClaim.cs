using Microsoft.AspNetCore.Identity;

namespace Lms.Identity.Infrastructure.Identity.Models
{
    public class ApplicationRoleClaim : IdentityRoleClaim<string>
    {
        public string? Description { get; set; }
        public string? Group { get; set; }
    }
}
