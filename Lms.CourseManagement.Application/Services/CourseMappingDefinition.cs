using Lms.CourseManagement.Application.Abstractions;
using Lms.Shared.Application.Sorting;

namespace Lms.CourseManagement.Application.Services
{
    public class CourseMappingDefinition<TSource, TDestination> : ICourseSortMappingDefinition
    {
        public required SortMapping[] Mappings { get; init; }
    }
}
