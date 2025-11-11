using Lms.SharedKernel.Domain;

namespace Lms.ContentDelivery.Domain.Entities
{
    public class StudentAccess : AggregateRoot<Guid>
    {
        public string StudentCode { get; private set; }
        private readonly List<Guid> _courseIds = [];
        public IReadOnlyCollection<Guid> CourseIds => _courseIds.AsReadOnly();


        private StudentAccess() { } // EF Core

        private StudentAccess(string studentCode)
        {
            StudentCode = studentCode;
        }

        public static StudentAccess Create(string studentCode, IEnumerable<Guid> courseIds)
        {
            if (string.IsNullOrEmpty(studentCode))
                throw new Exception("Student Code can not be empty"); // TODO: Use Custom Error

            var newStudentAccess = new StudentAccess(studentCode);
            newStudentAccess.AddCourse(courseIds);
            return newStudentAccess;
        }

        public void AddCourse(IEnumerable<Guid> courseIds)
        { 
            var courseIdSet = new HashSet<Guid>(courseIds);

            foreach (var courseId in courseIds)
            {
                if (courseIdSet.Add(courseId))
                {
                    _courseIds.Add(courseId);
                }
            }
        }

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
