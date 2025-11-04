namespace Lms.CourseManagement.Domain.ValueObjects
{
    public record LessonVideo 
    {
        public string Path { get; private set; }
        public string Title { get; private set; } 
        public string Thumbnail { get; private set; }
        public string Description { get; private set; }

        private LessonVideo()
        {
            
        }

        public LessonVideo(string path, string title,string thumbNail, string description)
        {
            Path = path;
            Title = title;
            Thumbnail = thumbNail;
            Description = description;
        }
    }

}