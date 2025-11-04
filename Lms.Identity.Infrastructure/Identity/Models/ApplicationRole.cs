using Microsoft.AspNetCore.Identity;

namespace Lms.Identity.Infrastructure.Identity.Models
{
    public class ApplicationRole : IdentityRole
    {
        public string? Description { get; set; }
    }
}
