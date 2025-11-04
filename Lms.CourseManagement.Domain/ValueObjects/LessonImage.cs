namespace Lms.CourseManagement.Domain.ValueObjects
{
    /// <summary>
    /// 
    /// </summary>
    public record LessonImage
    {
        // The unique path or URL to the stored image
        public string FileName { get; private set; }
        public string Path { get; private set; }
        public string Caption { get; private set; }

        private LessonImage() { } // EF core

        public LessonImage(string fileName, string path, string caption)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Path cannot be empty", nameof(path));  
            }
            FileName = fileName;
            Path = path;
            Caption = caption;
        }
    }
}
