using Lms.SharedKernel.Domain;

namespace Lms.CourseManagement.Domain.Entities
{
    public sealed class Course : AggregateRoot<Guid>
    {
        public IReadOnlyCollection<CourseModule> Modules => _modules.AsReadOnly();

        public string CourseTitle { get; private set; }
        public string CourseCode { get; private set; }
        public string Description { get; private set; }
        public string Category { get; private set; }
        public CourseStatus Status { get; private set; }
        public Guid InstructorId { get; private set; }
        public DateTime? PublishedOn { get; private set; }

        private readonly List<CourseModule> _modules = [];


        private Course() { } // EF core

        private Course(string courseTitle, string code, string description, string category, Guid instructorId)
        {
            Id = Guid.NewGuid();
            CourseTitle = courseTitle;
            CourseCode = code;
            Description = description;
            Category = category;
            InstructorId = instructorId;
        }

        public static Course Create(string courseTitle, string code, string description, string category, Guid instructorId)
        {
            if (string.IsNullOrWhiteSpace(courseTitle))
            { 
                throw new ArgumentException(nameof(courseTitle));
            }

            // Potential future rule:
            // if (instructor.HasTooManyCourses()) throw new DomainException(...);

            // Add Create new Course Event

            return new Course(courseTitle,  code, description, category, instructorId);

        }

        public void AddModuleToCourse(CourseModule module)
        {
            if (module == null)
            {
                throw new ArgumentNullException(nameof(module));
            }
            // auto assign ordering
            module.AssignOrder(_modules.Count + 1);
            _modules.Add(module);
        }

        public void AddLessonToModule(Guid moduleId, Lesson lesson)
        {
            var module = _modules.FirstOrDefault(m => m.Id == moduleId) ??
                throw new InvalidOperationException("Module not found in this course");

            module.AddLessonToModule(lesson);
        }

        public void Publish()
        {
            if (Status == CourseStatus.Published)
            {
                throw new InvalidOperationException("Course is already published");
            }
            Status = CourseStatus.Published;
            PublishedOn = DateTime.UtcNow;

            // Add a kernel-domain event to the list of events
            // This adds the event to be published internal CoursePublishEvent is of type IDomain
            AddDomainEvent(new CoursePublishedEvent(Id, CourseTitle, InstructorId, PublishedOn.Value));
        }

        public void Archive()
        {
            if (Status != CourseStatus.Published)
            {
                throw new InvalidOperationException("Only published course can be archived");
            }
            Status = CourseStatus.Archived;
        }
        public void MarkAsDeleted() => IsDeleted = true;

        public CourseModule GetModuleById(Guid moduleId)
        {
            var module = _modules.FirstOrDefault(m => m.Id == moduleId) ??
                throw new InvalidOperationException("Module not found in this course");
            return module;
        }
    }

    public enum CourseStatus
    {
        Draft,
        Published,
        Archived,
        Inactive
    }

}
