using Lms.SharedKernel.Domain;

namespace Lms.CourseDelivery.Domain.Entities
{
    public class Course : AggregateRoot<Guid>
    {
        public string CourseTitle { get; private set; }
        public string CourseCode { get; private set; }
        public string Description { get; private set; }
        public string Category { get; private set; }
        public Guid InstructorId { get; private set; }

        private Course() { } // EF Core

        private Course(string courseTitle, string courseCode, string description, string category, Guid instructorId)
        {
            CourseTitle = courseTitle;
            CourseCode = courseCode;
            Description = description;
            Category = category;
            InstructorId = instructorId;
        }

        public static Course Create(string courseTitle, string courseCode, string description, string category, Guid instructorId)
        {
            if (string.IsNullOrEmpty(courseTitle) || string.IsNullOrEmpty(courseCode))
                throw new Exception("Course title or course code cannot be null");

            return new Course(courseTitle, courseCode, description, category, instructorId);
        }
    }
}
