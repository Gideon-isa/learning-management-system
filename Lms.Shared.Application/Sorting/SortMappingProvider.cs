//using Lms.Shared.Abstractions.Sorting;

//namespace Lms.Shared.Application.Sorting
//{
//    public sealed class SortMappingProvider(IEnumerable<ISortMappingDefinition> sortMappingDefinitions)
//    {
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="TSource"></typeparam>
//        /// <typeparam name="TDestination"></typeparam>
//        /// <returns></returns>
//        public async Task<SortMapping[]> GetMappingsAsync<TSource, TDestination>()
//        {
//            SortMappingDefinition<TSource, TDestination>? sortMappingDefination = sortMappingDefinitions
//                .OfType<SortMappingDefinition<TSource, TDestination>>()
//                .FirstOrDefault();

//            if (sortMappingDefination == null) 
//            {
//                throw new InvalidOperationException($"The mapping from '{typeof(TSource).Name}' into '{typeof(TDestination).Name}' isn't defined");

//            };
                
                
//            return await Task.FromResult(sortMappingDefination.Mappings);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="TSource"></typeparam>
//        /// <typeparam name="TDestination"></typeparam>
//        /// <param name="sort"></param>
//        /// <returns></returns>
//        public async Task<bool> ValidateMappings<TSource, TDestination>(string? sort)
//        {
//            if(string.IsNullOrWhiteSpace(sort)) 
//                return false;
//            var sortFields = sort
//                .Split(',')
//                .Select(f => f.Trim()
//                .Split(' ')[0]) // gettting the course response property
//                .Where(f => !string.IsNullOrWhiteSpace(f))
//                .ToList();

//            SortMapping[] mapping = await GetMappingsAsync<TSource, TDestination>();
//            return sortFields.All(f => mapping.Any(m => m.SortField.Equals(f, StringComparison.OrdinalIgnoreCase)));
//        }

//    }
//}
