using Lms.CourseManagement.Application.Abstractions;
using Lms.CourseManagement.Application.Features.Course.Queries.GetAllCourse;
using Lms.CourseManagement.Application.Features.CourseFeatures.Sorting;
using Lms.CourseManagement.Domain.Entities;
using Lms.CourseManagement.Domain.Filters;
using Lms.CourseManagement.Domain.Repositories;
using Lms.CourseManagement.Infrastructure.DbContex;
using Lms.Shared.Application.Sorting;
using Microsoft.EntityFrameworkCore;

namespace Lms.CourseManagement.Infrastructure.Persistence
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CourseManagementDbContext _dbContext;
        private readonly SortMappingProvider _sortMappingProvider;

        public CourseRepository(CourseManagementDbContext dbContext, SortMappingProvider sortMappingProvider)
        {
            _dbContext = dbContext;
            _sortMappingProvider = sortMappingProvider;
        }

        public async Task AddAsync(Course course, CancellationToken cancellationToken)
        {
            await _dbContext.Courses.AddAsync(course, cancellationToken);
        }

        public async Task<IEnumerable<Course>> GetAllAsync(CourseFilter courseFilter, CancellationToken cancellationToken)
        {
            SortMapping[] mappings =  _sortMappingProvider.GetMappings<GetCoursesQuery, Course>();
            var courses = _dbContext.Courses
                    .Where(c => courseFilter.Search == null ||
                            c.CourseTitle.Contains(courseFilter.Search, StringComparison.CurrentCultureIgnoreCase) ||
                            c.Description != null && c.Description.Contains(courseFilter.Search, StringComparison.CurrentCultureIgnoreCase))
                    .Where(c => courseFilter.CourseCode == null || c.CourseCode.ToLower().Equals(courseFilter.CourseCode, StringComparison.OrdinalIgnoreCase))
                    .ApplySort(courseFilter.Sort, mappings);

            return await courses.Skip((courseFilter.Page - 1) * courseFilter.PageSize)
                                .Take(courseFilter.PageSize) 
                                .ToListAsync(cancellationToken);
           
        }

        public async Task<Course?> GetByIdAsync(Guid courseId, CancellationToken cancellationToken)
        {
            // Using Eager Loading to load related Modules and Lessons
            return await _dbContext.Courses
                .Include(c => c.Modules)
                .ThenInclude(m => m.Lessons)
                .FirstOrDefaultAsync(c => c.Id == courseId, cancellationToken);
        }

        public Task UpdateAsync(Course course, CancellationToken cancellationToken)
        {
            _dbContext.Courses.Update(course);
            return Task.CompletedTask;
        }
    }
}
