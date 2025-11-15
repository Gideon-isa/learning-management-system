namespace Lms.CourseManagement.Application.Models
{
    public class VideoMetadata
    {
        public Guid Id { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }


        public VideoMetadata() { } // EF Core

        public VideoMetadata(Guid id, string filePath, string contentType, long fileSize)
        {
            Id = id;
            FilePath = filePath;
            ContentType = contentType;
            FileSize = fileSize;
        }
    }

}
