namespace Lms.Shared.IntegrationEvents.Identity
{
    public sealed record CreateInstructorPublishedIntegrationEvent(
        Guid Id,
        string FirstName, 
        string LastName, 
        string DisplayName, 
        string ProfileImageUrl,
        string Bio,
        string Department, 
        string Institution, 
        string StaffId) { }
}
