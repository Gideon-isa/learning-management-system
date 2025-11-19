using Lms.CourseManagement.Domain.ValueObjects;
using Lms.SharedKernel.Domain;

namespace Lms.CourseManagement.Domain.Entities
{
    public class Content : Entity<Guid>
    {
        private readonly List<LessonNote> _notes = [];
        private readonly List<LessonImage> _images = [];
        private readonly List<LessonVideo> _videos = [];
        private readonly List<LessonTagReference> _tags = [];
        

        private Content() { } // EF core 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="title"></param>
        /// <param name="order"></param>
        private Content(string title, string description, TimeSpan courseDuration)
        {
            //Id = Guid.NewGuid();
            //ModuleId = moduleId;
            Title = title;
            Description = description;
            Duration = courseDuration;
        }
  
        public string Title { get; private set; } = string.Empty;
        public int Order { get; private set; }
        public TimeSpan Duration { get; private set; }
        public string Description { get; private set; } = string.Empty;

        public IReadOnlyCollection<LessonNote> Notes => _notes.AsReadOnly();
        public IReadOnlyCollection<LessonImage> Images => _images.AsReadOnly();
        public IReadOnlyCollection<LessonVideo> Videos => _videos.AsReadOnly();
        public IReadOnlyCollection<LessonTagReference> Tags => _tags.AsReadOnly();

        public void AssignOrder(int order) => Order = order;

        public static Content Create(
            string lessonTitle, 
            string lessonDescription, 
            TimeSpan lessonCourseDuration, 
            Guid videoId,
            string videoPath, 
            string videoTitle, 
            string videoThumbNail,
            string videoDescription, 
            IEnumerable<LessonImage> lessonImages, 
            IEnumerable<LessonTag> lessonTags)
        {
            if (string.IsNullOrWhiteSpace(lessonTitle))
                throw new ArgumentException("Title is required. ", nameof(lessonTitle));

            if (lessonCourseDuration <= TimeSpan.Zero)
                throw new Exception("Content duration must be positive.");

            var newLesson = new Content(lessonTitle, lessonDescription, lessonCourseDuration);
            newLesson.AddVideo(videoId, videoPath, videoTitle, videoThumbNail, videoDescription);
            newLesson.AddImage(lessonImages);
            newLesson.AddTags(lessonTags );

            return newLesson;
        }


        internal void AddNote(string content, string title, string description, TimeSpan courseDuration)
        {
            _notes.Add(new LessonNote(content, title));
            //UpdatedAt = DateTime.UtcNow;
        }

        internal void AddImage(IEnumerable<LessonImage> lessonImages)
        {
            foreach (var lessonImage in lessonImages)
            {
                _images.Add(lessonImage);           
            }
            //UpdatedAt = DateTime.UtcNow;
        }

        public void AddVideo(Guid videoId, string path, string title, string thumbNail, string description)
        {
            _videos.Add(new LessonVideo(videoId, path, title, thumbNail, description));
            //UpdatedAt = DateTime.UtcNow;
        }

        internal void AddTags(IEnumerable<LessonTag> lessonTags)
        {
            var existingTags = _tags.Select(t => t.TagId).ToHashSet();
            foreach (var tag in lessonTags)
            {
              if ( tag.TagName == null || tag.Id == Guid.Empty) 
                    throw new ArgumentNullException(nameof(tag)); // TODO: improve exception

                if (existingTags.Add(tag.Id))
                {
                    _tags.Add(new LessonTagReference(tag.Id, tag.TagName));
                }
            }              
        }
    }
}
