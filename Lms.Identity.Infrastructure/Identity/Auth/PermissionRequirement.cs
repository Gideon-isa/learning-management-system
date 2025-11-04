using Microsoft.AspNetCore.Authorization;

namespace Lms.Identity.Infrastructure.Identity.Auth
{
    public class PermissionRequirement(string permission) : IAuthorizationRequirement
    {
        public string Permission { get; } = permission;
    }
}
