namespace Lms.Api.Contracts.Enrollment
{
    public class CreateStudentEnrollmentRequest 
    {
        //public Guid CourseId { get; set; }
        public List<Guid> StudentIds { get; set; } = [];
    }
}
