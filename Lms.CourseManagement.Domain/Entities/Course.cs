using Lms.CourseManagement.Domain.Events;
using Lms.CourseManagement.Domain.Exceptions;
using Lms.SharedKernel.Domain;

namespace Lms.CourseManagement.Domain.Entities
{
    public sealed class Course : AggregateRoot<Guid>
    {
        public IReadOnlyCollection<CourseModule> Modules => _modules.AsReadOnly();

        public string CourseTitle { get; private set; }
        public string CourseCode { get; private set; }
        public string Description { get; private set; }
        public Guid CategoryId { get; private set; }
        public CourseStatus Status { get; private set; }
        public Guid InstructorId { get; private set; }
        public DateTime? PublishedOn { get; private set; }

        private readonly List<CourseModule> _modules = [];


        private Course() { } // EF core

        private Course(string courseTitle, string code, string description, Guid categoryId, Guid instructorId)
        {
            Id = Guid.NewGuid();
            CourseTitle = courseTitle;
            CourseCode = code;
            Description = description;
            CategoryId = categoryId;
            InstructorId = instructorId;
        }

        public static Course Create(string courseTitle, string code, string description, Guid categoryId, Guid instructorId)
        {
            if (string.IsNullOrWhiteSpace(courseTitle))
            { 
                throw new ArgumentException(nameof(courseTitle));
            }

            // Potential future rule:
            // if (instructor.HasTooManyCourses()) throw new DomainException(...);

            // Add Create new Course Event
            var newCourse = new Course(courseTitle, code, description, categoryId, instructorId);
            newCourse.GenerateCourseCode();
            return newCourse;

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

        public void AddLessonToModule(Guid moduleId, Content lesson)
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
            // This adds the event to be published internal CoursePublishEvent is of type IDomain\
            AddDomainEvent(new CoursePublishedEvent(Id, CourseTitle, CourseCode, CategoryId, 
                InstructorId, PublishedOn.Value, [..Modules]));
        }

        public void Archive()
        {
            if (Status != CourseStatus.Published)
                throw new InvalidOperationException("Only published course can be archived");
            Status = CourseStatus.Archived;
        }

        public void MarkAsDeleted() 
        {
            if (IsPublishd()) 
                throw new PublishedCourseException("Published course already published and cannot be deleted.");
            IsDeleted = true; 
        }

        public bool IsPublishd() => Status == CourseStatus.Published;

        public CourseModule GetModuleById(Guid moduleId)
        {
            var module = _modules.FirstOrDefault(m => m.Id == moduleId) ??
                throw new InvalidOperationException("Module not found in this course");
            return module;
        }

        public void RemoveModule(Guid moduleId)
        {
            var module = _modules.FirstOrDefault(m => m.Id == moduleId) 
                ?? throw DomainException.Create<CourseModuleNotFoundException>("module not found");

            _modules.Remove(module);

            if (IsPublishd())
                AddDomainEvent(new DeletedPublishedCourseModuleEvent(Id, moduleId));
        }

        public void GenerateCourseCode()
        {
            if (string.IsNullOrWhiteSpace(CourseTitle))
                throw new ArgumentException("Prefix cannot be null or empty", nameof(CourseTitle));
             var courseTitle = CourseTitle.Trim().Split(" ")[0][..3];
            CourseCode = $"{CourseTitle.ToUpperInvariant()}-{Guid.NewGuid().ToString().Split('-')[0].ToUpperInvariant()}";
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
