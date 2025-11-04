using Lms.Identity.Infrastructure.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Lms.Identity.Infrastructure.Identity.Auth
{
    public class ShouldHavePermissionAttribute : AuthorizeAttribute
    {
        public ShouldHavePermissionAttribute(string action, string feature)
        {
            Policy = ApplicationPermission.NameFor(action, feature);
        }
    }
}
