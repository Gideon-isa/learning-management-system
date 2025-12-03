using Lms.SharedKernel.Domain;

namespace Lms.ContentDelivery.Domain.Entities
{
    public class CourseModuleContent : Entity<Guid>
    {
        private readonly List<LessonContent> _lessons = [];
        public string Title { get; private set; }
        public int Order { get; private set; }
        public string Description { get; private set; }
        public IReadOnlyCollection<LessonContent> Lessons => _lessons.AsReadOnly();

        private CourseModuleContent() { } // EF core

        private CourseModuleContent(Guid id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }

        public static CourseModuleContent Create(Guid id, string title, string description)
        {

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title is required. ", nameof(title));
            }
            //Id = Guid.NewGuid();
            return new CourseModuleContent(id, title, description);
        }

        public void AssignOrder(int order) => Order = order;

        public void AddLessonToModule(LessonContent lesson)
        {
            if (lesson == null)
            {
                throw new ArgumentNullException(nameof(lesson));
            }
            lesson.AssignOrder(_lessons.Count + 1);

            _lessons.Add(lesson);
        }

        //public void AddLessonsToModule(List<LessonContent> lessons)
        //{
        //    if (lessons == null)
        //        throw new ArgumentNullException(nameof(lessons));

        //    //lessons.ForEach(_lessons.Add);

        //    foreach (var lesson in _lessons)
        //    {
        //        _lessons.Add(lesson);
        //        lesson.AssignOrder(_lessons.Count + 1);
        //    } 

        //}
    }
}
