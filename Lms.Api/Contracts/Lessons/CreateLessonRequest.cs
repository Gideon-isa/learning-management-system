using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace Lms.Api.Contracts.Lessons
{
    public class CreateLessonRequest
    {
        [FromForm]
        public Guid CourseId { get; set; }
        [FromForm]
        public Guid ModuleId { get; set; }
        [FromForm]
        public string Title { get; set; } = string.Empty;
        [FromForm]
        public int Order { get; set; }
        [FromForm]
        public string Duration { get; set; } = "00:00:00";
        [FromForm]
        public string LessonDescription { get; set; } = string.Empty;

        // Flatten VideoUploadDto
        [FromForm]
        public IFormFile? VideoFile { get; set; }

        [FromForm]
        public string VideoTitle { get; set; } = string.Empty;

        [FromForm]
        public string VideoDescription { get; set; } = string.Empty;

        [FromForm]
        public string VideoThumbNail { get; set; } = string.Empty;

        //Flatten ImageUploadDto properties
        [FromForm]
        public List<IFormFile>? Images { get; set; } = [];
        [FromForm]
        public List<string>? ImageCaptions { get; set; } = [];

        [FromForm]
        public string[]? LessonTagIds { get; set; }

        [BindNever]
        [SwaggerSchema(ReadOnly = true, Description = "List of Tag IDs (parsed from comma, separated string")]
        public List<Guid>? TagIds =>
            LessonTagIds == null 
            ? [] : [.. LessonTagIds.
            Where(x => !string.IsNullOrWhiteSpace(x))
            .SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries)).Select(Guid.Parse)];

        [BindNever]
        [SwaggerSchema(ReadOnly = true, Description ="List if image caption")]
        public List<string>? ImageCaptionList =>
            ImageCaptions == null
            ? [] : [.. ImageCaptions.Where(x => !string.IsNullOrWhiteSpace(x)).SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries))];



    }

}
