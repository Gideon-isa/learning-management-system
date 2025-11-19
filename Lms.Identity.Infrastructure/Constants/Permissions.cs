using Lms.Shared.Security.Permissions;
using System.Collections.ObjectModel;

namespace Lms.Identity.Infrastructure.Constants
{
    public static class Permissions
    {
        private static readonly ApplicationPermission[] _permissions =
        [
            // Permission for the users
            new ApplicationPermission(PermissionConstants.Create, LearningFeatureConstants.Users, "Create Users", "SystemAccess", IsInstructor: true),
            new ApplicationPermission(PermissionConstants.Update, LearningFeatureConstants.Users, "Update Users", "SystemAccess", IsInstructor: true),
            new ApplicationPermission(PermissionConstants.Delete, LearningFeatureConstants.Users, "Delete Users", "SystemAccess", IsInstructor: true),
            new ApplicationPermission(PermissionConstants.Read, LearningFeatureConstants.Users, "Read Student", "SystemAccess", IsInstructor: true),

            // Permission to manage Students
            new ApplicationPermission(PermissionConstants.Read, LearningFeatureConstants.Students, "Read Student", "Academics", IsInstructor: true),

            new ApplicationPermission(PermissionConstants.Delete, LearningFeatureConstants.Students, "Delete Student", "Academics", IsInstructor: true),
            
            // Permission to modify user roles
            new ApplicationPermission(PermissionConstants.Read, LearningFeatureConstants.UserRoles, "Read User Roles", "SystemAccess"),
            new ApplicationPermission(PermissionConstants.Update, LearningFeatureConstants.UserRoles, "Update User Roles", "SystemAccess"),

            // Permission for roles
            new ApplicationPermission(PermissionConstants.Read, LearningFeatureConstants.Roles, "Read Roles", "SystemAccess"),
            new ApplicationPermission(PermissionConstants.Create, LearningFeatureConstants.Roles, "Create Roles", "SystemAccess"),
            new ApplicationPermission(PermissionConstants.Update, LearningFeatureConstants.Roles, "Update Roles", "SystemAccess"),
            new ApplicationPermission(PermissionConstants.Delete, LearningFeatureConstants.Roles, "Delete Roles", "SystemAccess"),

            // Claims
            new ApplicationPermission(PermissionConstants.Read, LearningFeatureConstants.RoleClaim, "Read Roles Claims/Permissions", "SystemAccess"),
            new ApplicationPermission(PermissionConstants.Update, LearningFeatureConstants.RoleClaim, "Update Roles Claims/Permissions", "SystemAccess"),

            // Permission to modify Learning Paths
            new ApplicationPermission(PermissionConstants.Read, LearningFeatureConstants.LearningPath, "Read Learning Path", "Academics"),

            new ApplicationPermission(PermissionConstants.Create, LearningFeatureConstants.LearningPath, "Create Learning Path", "Academics", IsInstructor: true ),
            new ApplicationPermission(PermissionConstants.Update, LearningFeatureConstants.LearningPath, "Update Learning Path", "Academics", IsInstructor: true),
            new ApplicationPermission(PermissionConstants.Delete, LearningFeatureConstants.LearningPath, "Delete Learning Path", "Academics", IsInstructor: true),

            // Permission to modify Course
            new ApplicationPermission(PermissionConstants.Read, LearningFeatureConstants.Course, "Read Course", "Academics"),

            new ApplicationPermission(PermissionConstants.Create, LearningFeatureConstants.Course, "Create Course", "Academics",IsInstructor: true ),
            new ApplicationPermission(PermissionConstants.Update, LearningFeatureConstants.Course, "Update Course", "Academics", IsInstructor: true),
            new ApplicationPermission(PermissionConstants.Delete, LearningFeatureConstants.Course, "Delete Course", "Academics", IsInstructor: true),

            // Permission to modify Lesson
            new ApplicationPermission(PermissionConstants.Read, LearningFeatureConstants.Lesson, "Read Leasson", "Academics"),

            new ApplicationPermission(PermissionConstants.Create, LearningFeatureConstants.Lesson, "Create Lesson", "Academics", IsInstructor: true),
            new ApplicationPermission(PermissionConstants.Update, LearningFeatureConstants.Lesson, "Update Lesson", "Academics", IsInstructor: true),
            new ApplicationPermission(PermissionConstants.Delete, LearningFeatureConstants.Lesson, "Delete Lesson", "Academics", IsInstructor: true),

            new ApplicationPermission(PermissionConstants.RefreshToken, LearningFeatureConstants.Tokens, "Generate Refresh Token", "SystemAccess")
        ];

        public static IReadOnlyList<ApplicationPermission> All { get; } = new ReadOnlyCollection<ApplicationPermission>(_permissions);
        public static IReadOnlyList<ApplicationPermission> Instructor { get; }
            = new ReadOnlyCollection<ApplicationPermission>([.. _permissions.Where(p => p.IsInstructor)]);

        public static IReadOnlyList<ApplicationPermission> Student { get; }
           = new ReadOnlyCollection<ApplicationPermission>([.. _permissions.Where(p => !p.IsInstructor) ]);

        //public static IReadOnlyList<ApplicationPermission> Admin { get; }
        //    = new ReadOnlyCollection<ApplicationPermission>([.. _allPermssions.Where(p => !p.IsRoot)]);

    }
}
