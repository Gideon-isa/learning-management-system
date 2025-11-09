using Lms.SharedKernel.Domain;

namespace Lms.CourseDelivery.Domain.Entities
{
    public class StudentAccess : AggregateRoot<Guid>
    {
        public string StudentCode { get; private set; }
        private readonly List<Guid> _courseIds = [];
        public IReadOnlyCollection<Guid> CourseIds => _courseIds.AsReadOnly();

        public void GrantAccess(Guid courseId)
        {
            if (!_courseIds.Contains(courseId))
            {
                _courseIds.Add(courseId);
            }
        }

        public void RevokeAccess(Guid courseId) 
        { 
            _courseIds.Remove(courseId);
        }
    }
}
