namespace Lms.CourseManagement.Domain.ValueObjects
{
    public record LessonNote
    {
        public string Content { get; private set; }
        public string Title { get; private set; }

        private LessonNote() // EF Core
        {
            
        }

        public LessonNote(string content, string title)
        {
            Content = content;
            Title = title;
        }
 
    }
}
