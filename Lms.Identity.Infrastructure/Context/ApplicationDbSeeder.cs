using Lms.Identity.Infrastructure.Constants;
using Lms.Identity.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Lms.Identity.Infrastructure.Context
{
    public class ApplicationDbSeeder(
        RoleManager<ApplicationRole> roleManager,
        UserManager<ApplicationUser> userManager,
        UserIdentityDbContext applicationDbContext)
    {
        private readonly RoleManager<ApplicationRole> _roleManager = roleManager;   
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly UserIdentityDbContext _applicationDbContext = applicationDbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task InitializeDatabaseAsync(CancellationToken cancellation)
        {
            if (_applicationDbContext.Database.GetMigrations().Any())
            { 
                if((await _applicationDbContext.Database.GetPendingMigrationsAsync(cancellation)).Any())
                {
                    await _applicationDbContext.Database.MigrateAsync(cancellation);
                }

                // seeding
                // checking if database has been setup
                if (await _applicationDbContext.Database.CanConnectAsync(cancellation))
                {
                    await InitializeDefaultRolesAsync(cancellation);
                    await InitializeAdminUserAsync();
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task InitializeDefaultRolesAsync(CancellationToken cancellation)
        {
            // Implement role seeding logic
            foreach (var roleName in RoleConstants.DefaultRoles)
            {
                if (await _roleManager.Roles.SingleOrDefaultAsync(role => role.Name == roleName, cancellation) is not ApplicationRole applicationRole)
                {
                    applicationRole = new ApplicationRole
                    {
                        Name = roleName,
                        Description = $"{roleName} Role"
                    };
                    await _roleManager.CreateAsync(applicationRole);
                }

                // assign permissions
                if (roleName == RoleConstants.Student)
                {
                    await AssignPermissionsToRole(Permissions.Student, applicationRole, cancellation);
                }
                else if (roleName == RoleConstants.Instructor)
                {
                    await AssignPermissionsToRole(Permissions.Instructor, applicationRole, cancellation);

                }
                else if (roleName == RoleConstants.Admin)
                {
                    await AssignPermissionsToRole(Permissions.All, applicationRole, cancellation);
                }

            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="rolePermissions"></param>
        /// <param name="role"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        private async Task AssignPermissionsToRole(IReadOnlyList<ApplicationPermission> rolePermissions, ApplicationRole role, CancellationToken cancellation)
        {
            var currentClaims = await _roleManager.GetClaimsAsync(role);
            var existingPermissions = currentClaims.Where(c => c.Type == ClaimConstants.Permission).Select(c => c.Value).ToHashSet();

            var claims = rolePermissions.Where(p => !existingPermissions.Contains(p.Name)).Select(p => new ApplicationRoleClaim
            {
                RoleId = role.Id,
                ClaimType = ClaimConstants.Permission,
                ClaimValue = p.Name,
                Description = p.Description,
                Group = p.Group,
            }).ToList();

            if (claims.Count > 0)
            {
                await _applicationDbContext.RoleClaims.AddRangeAsync(claims, cancellation);
                await _applicationDbContext.SaveChangesAsync(cancellation);
            }
        }

        private async Task InitializeAdminUserAsync()
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(user => user.Email == "admin@insightful.com");

            

            if (user is null)
            {
                user = new ApplicationUser
                {
                    FirstName = AdminUser.FirstName,
                    LastName = AdminUser.LastName,
                    Email = AdminUser.Email,
                    UserName = AdminUser.Email,
                    EmailConfirmed = true,
                    NormalizedEmail = AdminUser.Email.ToUpperInvariant(),
                    NormalizedUserName = AdminUser.Email.ToUpperInvariant(),
                    PhoneNumberConfirmed = true,
                    IsActive = true,
                };

                user.PasswordHash =
                     new PasswordHasher<ApplicationUser>()
                    .HashPassword(user, AdminUser.Password);

                await _userManager.CreateAsync(user);
            }


            if (!await _userManager.IsInRoleAsync(user, RoleConstants.Admin))
            {
                await _userManager.AddToRoleAsync(user, RoleConstants.Admin);
            }
        }
    }
}
