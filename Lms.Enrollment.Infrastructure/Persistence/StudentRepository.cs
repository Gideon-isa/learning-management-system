using Lms.Enrollment.Domain.Entities;
using Lms.Enrollment.Domain.Respositories;
using Lms.Enrollment.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Lms.Enrollment.Infrastructure.Persistence
{
    public class StudentRepository : IStudentRepository
    {
        private readonly EnrollmentDbContext _enrollmentDbContext;

        public StudentRepository(EnrollmentDbContext enrollmentDbContext)
        {
            _enrollmentDbContext = enrollmentDbContext;
        }

        public async Task CreateAsync(Student student, CancellationToken cancellationToken)
        {
            await _enrollmentDbContext.Students.AddAsync(student, cancellationToken);
            
        }

        public async Task<bool> DoesStudentExistAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _enrollmentDbContext.Students.Where(u => u.Id == id).ToListAsync();
            return result.Count > 0;
        }

        public async Task<List<Student>> GetAllStudents(CancellationToken cancellationToken)
        {
            return await _enrollmentDbContext.Students.ToListAsync(cancellationToken); 
        }

        public async Task<Student?> GetStudentByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _enrollmentDbContext.Students.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<List<Student>> GetStudentsByIds(List<Guid> studentIds, CancellationToken cancellationToken)
        {
            if (studentIds == null || studentIds.Count == 0) 
            { 
                return new List<Student>(); 
            }
            return await _enrollmentDbContext.Students
                    .Where(u => studentIds.Contains(u.Id))
                    .ToListAsync(cancellationToken);
        }
    }
}
