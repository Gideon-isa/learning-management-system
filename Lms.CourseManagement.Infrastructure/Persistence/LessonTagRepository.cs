using Lms.CourseManagement.Domain.Entities;
using Lms.CourseManagement.Domain.Repositories;
using Lms.CourseManagement.Infrastructure.DbContex;
using Microsoft.EntityFrameworkCore;

namespace Lms.CourseManagement.Infrastructure.Persistence
{
    public class LessonTagRepository : ILessonTagRespository
    {
        private readonly CourseManagementDbContext _dbContext;

        public LessonTagRepository(CourseManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(LessonTag lessonTag, CancellationToken cancellationToken)
        {
            await _dbContext.LessonTags.AddAsync(lessonTag, cancellationToken);
        }

        public Task DeleteAsync(Guid tagId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<LessonTag>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.LessonTags.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<LessonTag?> GetByIdAsync(Guid tagId, CancellationToken cancellationToken)
        {
            var tag = await _dbContext.LessonTags.FindAsync([tagId], cancellationToken);
            return tag;
        }

        public async Task<List<LessonTag>> GetByIdsAsync(IEnumerable<Guid> tagIds, CancellationToken cancellationToken)
        {
            var idList = tagIds as IList<Guid> ?? [.. tagIds];
            if(!idList.Any())
                return [];

            return await _dbContext.LessonTags
                .AsNoTracking()
                .Where(tag => idList.Contains(tag.Id))
                .ToListAsync(cancellationToken);

        }
    }
}
