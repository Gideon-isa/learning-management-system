namespace Lms.ContentDelivery.Domain.ValueObjects
{
    public class LessonVideo
    {
        public string Path { get; private set; }
        public string Title { get; private set; }
        public string Thumbnail { get; private set; }
        public string Description { get; private set; }

        private LessonVideo() { }

        public LessonVideo(string path, string title, string thumbNail, string description)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException("Path or title cannot be empty");

            Path = path;
            Title = title;
            Thumbnail = thumbNail;
            Description = description;
        }
    }
}
