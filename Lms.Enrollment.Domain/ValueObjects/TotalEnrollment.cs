namespace Lms.Enrollment.Domain.ValueObjects
{
    public record TotalEnrollment(int EnrollmentCount)
    {
        private TotalEnrollment() : this (0){} // EF Core

        public int NextEnrollmentCount() => EnrollmentCount + 1;
    }
}
