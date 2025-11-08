using Lms.Enrollment.Domain.Enums;
using Lms.SharedKernel.Domain;

namespace Lms.Enrollment.Domain.Entities
{
    public class AvailableCourse : AggregateRoot<Guid>
    {
        public Guid CourseId { get; private set; }
        public string CourseTitle { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string InstructorName { get; private set; } = string.Empty;
        public string ThumbNail { get; private set; } = string.Empty; 
        public bool IsActive { get; private set; }
        public CourseAvailabiltyStatus AvailabiltyStatus { get; private set; }

        private AvailableCourse() { } // EF Core

        private AvailableCourse(Guid courseId, string title, string description, string instructorName, string thumbnail)
        {
            CourseId = courseId;
            CourseTitle = title;
            Description = description;
            InstructorName = instructorName;
            ThumbNail = thumbnail;
        }

        public static AvailableCourse Create(Guid courseId, string title, string description, string instructorName, string thumbnail) 
            => new(courseId, title, description, instructorName, thumbnail);


       public void Unpublish()
        {
            if (AvailabiltyStatus == CourseAvailabiltyStatus.Unpublished)
                throw new InvalidOperationException("Course is already unpublished.");
            AvailabiltyStatus = CourseAvailabiltyStatus.Unpublished;
        }
    }
}
