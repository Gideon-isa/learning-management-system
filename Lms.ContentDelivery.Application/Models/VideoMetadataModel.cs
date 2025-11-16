namespace Lms.ContentDelivery.Application.Models
{
    public class VideoMetadataModel
    {
        public Guid Id { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }

        private VideoMetadataModel() { }
    }

}
