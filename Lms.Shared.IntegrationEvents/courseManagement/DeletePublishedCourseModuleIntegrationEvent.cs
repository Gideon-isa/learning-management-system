namespace Lms.Shared.IntegrationEvents.courseManagement
{
    public sealed record DeletePublishedCourseModuleIntegrationEvent(Guid CourseId, Guid ModuleId);

}
