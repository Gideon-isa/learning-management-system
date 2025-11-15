using Lms.Api.Contracts.Lessons;
using Lms.CourseManagement.Application.Features.Lesson.Command;

namespace Lms.Api.Extensions
{
    public static class LessonExtensions
    {
        public static CreateLessonCommand ToCreateCommand(this CreateLessonRequest request)
        {
            var videoDto = request.VideoFile is not null 
                ? new VideoFileDto
            {
                Content = request?.VideoFile?.OpenReadStream() ?? null!,
                FileName = request.VideoFile.FileName,
                Title = request.VideoTitle,
                ContentType = request.VideoFile.ContentType,
                FileSize = request.VideoFile.Length,
                ThumbNail = request.VideoThumbNail,
                Description = request.VideoDescription,
            } 
            : null;

            List<ImageFileDto> images = [];

            var captionCount = request.ImageCaptionList.Count;
            var imageCount = request.Images.Count;

            if (request.ImageCaptionList == null 
                || request.Images == null
                || request.ImageCaptionList.Count != request.Images.Count)
                throw new InvalidOperationException("Each Uploaded image must have a corresponding caption");


            for (int i = 0; i < request?.Images?.Count; i++)
            {
                var image = request.Images[i];
                var caption = request.ImageCaptionList[i];

                var newImage = new ImageFileDto
                {
                    Content = image.OpenReadStream(),
                    FileName = image.FileName,
                    Caption = caption
                };
                images.Add(newImage);
            }
            
            var command = new CreateLessonCommand
            {
                CourseId = request.CourseId,
                ModuleId = request.ModuleId,
                Title = request.Title,
                Order = request.Order,
                Duration = TimeSpan.Parse(request.Duration),
                Description = request.LessonDescription,
                Video = videoDto,
                Images = images,
                TagIds = request.TagIds
            };
            return command;
        }
    }
}
