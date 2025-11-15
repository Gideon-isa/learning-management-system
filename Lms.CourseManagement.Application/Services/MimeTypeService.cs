using Lms.Shared.Abstractions.FileStorage;

namespace Lms.CourseManagement.Application.Services
{
    public class MimeTypeService : IMimeTypeService
    {
        public string GetMimeType(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                ".mp4" => "video/mp4",
                ".mov" => "video/quicktime",
                ".avi" => "video/x-msvideo",
                ".wmv" => "video/x-ms-wmv",
                _ => "application/octet-stream",
            };
        }
    }
}
