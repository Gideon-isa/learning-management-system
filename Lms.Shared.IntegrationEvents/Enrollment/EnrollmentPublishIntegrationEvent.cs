namespace Lms.Shared.IntegrationEvents.Enrollment
{
    public sealed record EnrollmentPublishIntegrationEvent(string StudentCode, Guid CourseId) { }

}
