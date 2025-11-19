using Lms.CourseManagement.Application.Abstractions;
using Lms.CourseManagement.Application.Services;
using Lms.Shared.Abstractions.Sorting;
using Lms.Shared.Application.Sorting;

namespace Lms.CourseManagement.Application.Features.CourseFeatures.Sorting
{
    public sealed class SortMappingProvider(IEnumerable<ICourseSortMappingDefinition> sortMappingDefinitions)
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <returns></returns>
        public SortMapping[] GetMappings<TSource, TDestination>()
        {
            CourseMappingDefinition<TSource, TDestination>? sortMappingDefination = sortMappingDefinitions
                .OfType<CourseMappingDefinition<TSource, TDestination>>()
                .FirstOrDefault();

            if (sortMappingDefination == null) 
            {
                throw new InvalidOperationException($"The mapping from '{typeof(TSource).Name}' into '{typeof(TDestination).Name}' isn't defined");

            };         
            return sortMappingDefination.Mappings;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="sort"></param>
        /// <returns></returns>
        public bool ValidateMappings<TSource, TDestination>(string? sort)
        {
            if(string.IsNullOrWhiteSpace(sort)) 
                return true;
            var sortFields = sort
                .Split(',')
                .Select(f => f.Trim()
                .Split(' ')[0]) // gettting the course response property
                .Where(f => !string.IsNullOrWhiteSpace(f))
                .ToList();

            SortMapping[] mapping = GetMappings<TSource, TDestination>();
            return sortFields.All(f => mapping.Any(m => m.SortField.Equals(f, StringComparison.OrdinalIgnoreCase)));
        }

    }
}
