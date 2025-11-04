using System.Collections.ObjectModel;

namespace Lms.Identity.Infrastructure.Constants
{
    public static class RoleConstants
    {
        public const string Admin = nameof(Admin);
        public const string Instructor = nameof(Instructor);
        public const string Student = nameof(Student);
        
        public static IReadOnlyList<string> DefaultRoles { get; } = new ReadOnlyCollection<string>(
        [
            Admin, Instructor, Student
        ]);

        public static bool IsDefaultRole(string roleName) => DefaultRoles.Contains(roleName);

    }
}
