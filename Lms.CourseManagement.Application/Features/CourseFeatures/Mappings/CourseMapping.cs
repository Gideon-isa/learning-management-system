using Lms.CourseManagement.Application.Features.Course.Queries.GetAllCourse;
using Lms.CourseManagement.Application.Services;
using Lms.Shared.Application.Sorting;

namespace Lms.CourseManagement.Application.Features.CourseFeatures.Mappings
{
    public static class CourseMapping
    {
        public static readonly CourseMappingDefinition<GetCoursesQuery, Domain.Entities.Course> Sortmapping = new()
        { 
            Mappings = 
            [
                new SortMapping(nameof(GetCoursesQuery.CourseTitle), nameof(Domain.Entities.Course.CourseTitle)),
                //new SortMapping(nameof(GetCoursesQuery.Category), nameof(Domain.Entities.Course.)),
                new SortMapping(nameof(GetCoursesQuery.CourseCode), nameof(Domain.Entities.Course.CourseCode))
            ] 
        };
    }
}
