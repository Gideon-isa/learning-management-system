using Lms.ContentDelivery.Domain.ValueObjects;
using Lms.SharedKernel.Domain;

namespace Lms.ContentDelivery.Domain.Entities
{
    public class LessonContent : Entity<Guid>
    {
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
        public string Description { get; set; }
        public readonly List<LessonVideo> _videos = [];

        public IReadOnlyCollection<LessonVideo> Videos => _videos.AsReadOnly();

        private LessonContent() { } // EF Core
        private LessonContent(string title, string description, TimeSpan courseDuration)
        {
            Title = title;
            Description = description;
            Duration = courseDuration;
        }


        public static LessonContent Create(
            string lessonTitle,
            string lessonDescription,
            TimeSpan lessonCourseDuration,
            string videoPath,
            string videoTitle,
            string videoThumbNail,
            string videoDescription)
        {
            if (string.IsNullOrWhiteSpace(lessonTitle))
                throw new ArgumentException("Title is required. ", nameof(lessonTitle));

            if (lessonCourseDuration <= TimeSpan.Zero)
                throw new Exception("Lesson duration must be positive.");

            var newLesson = new LessonContent(lessonTitle, lessonDescription, lessonCourseDuration);
            newLesson.AddVideo(videoPath, videoTitle, videoThumbNail, videoDescription);
            //newLesson.AddImage(lessonImages);
            //newLesson.AddTags(lessonTags);

            return newLesson;
        }


        private void AddVideo(string path, string title, string thumbNail, string description)
        {
            _videos.Add(new LessonVideo(path, title, thumbNail, description));
            //UpdatedAt = DateTime.UtcNow;
        }
    }
}
