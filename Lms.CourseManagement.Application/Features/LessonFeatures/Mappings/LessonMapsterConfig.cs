using Lms.CourseManagement.Application.Features.LessonFeatures.Dto;
using Mapster;

namespace Lms.CourseManagement.Application.Features.LessonFeatures.Mappings
{
    public static class LessonMapsterConfig
    {
        public static void RegisterLessonMappings()
        {
            // Add Mapster configurations for Lesson mappings here
            TypeAdapterConfig<Domain.Entities.Content, LessonResponse>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Videos, src => src.Videos.Adapt<List<LessonVideoResponse>>())
                .Map(dest => dest.Images, src => src.Images.Adapt<List<LessonImageResponse>>());

        }
    }
}
