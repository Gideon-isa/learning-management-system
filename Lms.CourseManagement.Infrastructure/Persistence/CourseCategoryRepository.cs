using Lms.CourseManagement.Domain.Entities;
using Lms.CourseManagement.Domain.Repositories;
using Lms.CourseManagement.Infrastructure.DbContex;
using Microsoft.EntityFrameworkCore;

namespace Lms.CourseManagement.Infrastructure.Persistence
{
    public class CourseCategoryRepository : ICourseCategoryRepository
    {
        private readonly CourseManagementDbContext _dbContext; 
        public CourseCategoryRepository(CourseManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(CourseCategory courseCategory, CancellationToken cancellationToken)
        {
            await _dbContext.CourseCategories.AddAsync(courseCategory, cancellationToken);
        }

        public async Task<IEnumerable<CourseCategory>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.CourseCategories.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<CourseCategory?> GetAsync(Guid courseCategoryId, CancellationToken cancellationToken)
        {
            return await _dbContext.CourseCategories.FirstOrDefaultAsync(cc => cc.Id == courseCategoryId, cancellationToken);
        }
    }
}
