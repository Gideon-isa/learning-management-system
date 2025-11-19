using Lms.SharedKernel.Domain;

namespace Lms.CourseManagement.Domain.Filters
{
    public class CourseFilter : ResultFilter
    {
        public string CourseTitle { get; set; }
        public string CourseCode { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}
