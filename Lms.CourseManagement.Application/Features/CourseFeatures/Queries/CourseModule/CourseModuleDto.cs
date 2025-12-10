namespace Lms.CourseManagement.Application.Features.CourseFeatures.Queries.CourseModule
{
    public class CourseModuleDto
    {
        public Guid Id { get; set; }
        public string CourseTitle { get; set; }
        public string CourseCode { get; set; }
        public List<CourseModuleDetailDto> Modules { get; set; }

        public CourseModuleDto()
        {

        }
    }

    public class CourseModuleDetailDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
