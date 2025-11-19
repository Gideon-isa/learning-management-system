using Lms.CourseManagement.Domain.ValueObjects;
using Lms.SharedKernel.Domain;


namespace Lms.CourseManagement.Domain.Entities
{
    public class CourseModule : Entity<Guid>
    {
        private CourseModule() { } // EF core

        private CourseModule(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public static CourseModule Create(string title, string description)
        {

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title is required. ", nameof (title)); 
            }
            //Id = Guid.NewGuid();
            return new CourseModule(title, description);
        }

        public void AssignOrder(int order) => Order = order;


        private readonly List<Content> _lessons = [];
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int Order { get; private set; }

        public IReadOnlyCollection<Content> Lessons => _lessons.AsReadOnly();

        // Module is the only Aggregate allowed to create Lessons
        //public Content AddLesson(string title, int order)
        //{
        //    var lesson = new Content(Id, title, order);
        //    _lessons.Add(lesson);
        //    return lesson;
        //}

        //public void AddLesson(Content lesson) 
        //{ 
        //    _lessons.Add(lesson);
        //}

        public void AddLessonToModule(Content lesson)
        {
            if (lesson == null)
            {
                throw new ArgumentNullException(nameof(lesson));
            }
            lesson.AssignOrder(_lessons.Count + 1);

            _lessons.Add(lesson);
        }   

        //public Content AddLesson
        //    (
        //    string lessonTitle, 
        //    int order, 
        //    string description, 
        //    TimeSpan duration, 
        //    string videoPath, 
        //    string videoTitle, 
        //    string videoThumbnail,
        //    string videoDescription,
        //    List<LessonImage> lessonImages,
        //    List<Guid> Tags)
        //{
        //    //var module = _modules.FirstOrDefault(m => m.Id == moduleId) ??
        //    //    throw new InvalidOperationException("Module not found in this course");

        //    var lesson = new Content(Id, lessonTitle, order, description, duration);

        //    lesson.AddVideo(videoPath, videoTitle, videoThumbnail, videoDescription);

        //    //foreach (var tagId in Tags)
        //    //{
        //    //    lesson.AddTags(tagId);
        //    //}

        //    lesson.AddImage(lessonImages);

        //    _lessons.Add(lesson);
        //    return lesson;

        //    //module.AddLesson(new Content(moduleId, lessonTitle, order));
        //}
    }
}
