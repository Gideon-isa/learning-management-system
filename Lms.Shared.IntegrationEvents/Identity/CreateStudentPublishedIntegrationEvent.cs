namespace Lms.Shared.IntegrationEvents.Identity
{
    public sealed record CreateStudentPublishedIntegrationEvent(
        Guid Id,
        string FirstName,
        string LastName,
        string Username,
        string StudentCode) { }
}
