namespace Lms.CourseManagement.Domain.ValueObjects
{
    public record LessonVideo 
    {
        public Guid VideoId { get; private set; } // technical identifier
        public string Path { get; private set; }
        public string Title { get; private set; } 
        public string Thumbnail { get; private set; }
        public string Description { get; private set; }

        private LessonVideo() { }

        public LessonVideo(Guid videoId, string path, string title,string thumbNail, string description)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException("Path or title cannot be empty");
            
            VideoId = videoId;
            Path = path;
            Title = title;
            Thumbnail = thumbNail;
            Description = description;
        }
    }

}