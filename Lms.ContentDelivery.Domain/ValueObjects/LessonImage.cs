namespace Lms.ContentDelivery.Domain.ValueObjects
{
    public class LessonImage
    {
        public Guid Id { get; set; }
        public string FileName { get; private set; }
        public string Path { get; private set; }
        public string Caption { get; private set; }

        private LessonImage() { }

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
