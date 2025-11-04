namespace Lms.Identity.Infrastructure.Constants
{
    public record ApplicationPermission(string Action, string Feature, string Description, string Group, bool IsInstructor = false, bool IsStudent = false)
    {
        public string Name => NameFor(Action, Feature);
        public static string NameFor(string action, string feature) => $"{ClaimConstants.Permission}.{feature}.{action}";
    }
}
