using Lms.SharedKernel.Domain;

namespace Lms.ContentDelivery.Domain.Entities
{
    public class CourseContent : AggregateRoot<Guid>
    {
        public IReadOnlyCollection<CourseModuleContent> Modules => _modules.AsReadOnly();
        public Guid CourseId { get; private set; }
        public string CourseTitle { get; private set; }
        public string CourseCode { get; private set; }
        public string CourseCategoryCode { get; private set; }
        public Guid InstructorId { get; private set; }

        public readonly List<CourseModuleContent> _modules = [];


        private CourseContent() { } // EF Core

        private CourseContent(Guid courseId, string courseTitle, string courseCode, string category, Guid instructorId)
        {
            CourseId = courseId;    
            CourseTitle = courseTitle;
            CourseCode = courseCode;
            CourseCategoryCode = category;
            InstructorId = instructorId;
        }

        public static CourseContent Create(Guid id, string courseTitle, string courseCode, string courseCategoryCode, Guid instructorId)
        {
            if (string.IsNullOrEmpty(courseTitle) || string.IsNullOrEmpty(courseCode))
                throw new Exception("Course title or course code cannot be null");

            return new CourseContent(id, courseTitle, courseCode, courseCategoryCode, instructorId);
        }

        public void AddModuleToCourse(CourseModuleContent module)
        {
            if (module == null)
            {
                throw new ArgumentNullException(nameof(module));
            }
            // auto assign ordering
            module.AssignOrder(_modules.Count + 1);
            _modules.Add(module);
        }

        public void RemoveModuleFromCourse(Guid moduleId)
        {
            var module = _modules.FirstOrDefault(m => m.Id == moduleId) ??
                throw new InvalidOperationException("Module not found in this course content");
            _modules.Remove(module);
        }
    }
}
