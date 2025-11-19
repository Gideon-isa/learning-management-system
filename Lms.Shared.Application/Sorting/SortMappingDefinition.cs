using Lms.Shared.Abstractions.Sorting;

namespace Lms.Shared.Application.Sorting
{
    public class SortMappingDefinition<TSource, TDestination> : ISortMappingDefinition
    {
        public required SortMapping[] Mappings { get; init; }
    }
}
