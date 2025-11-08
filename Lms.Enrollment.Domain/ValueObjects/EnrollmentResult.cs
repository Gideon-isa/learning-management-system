namespace Lms.Enrollment.Domain.ValueObjects
{
    public record EnrollmentResult (IReadOnlyCollection<StudentSummary> Enrolled, IReadOnlyCollection<StudentSummary> Skipped);

}
