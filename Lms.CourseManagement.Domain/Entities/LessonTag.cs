using Lms.SharedKernel.Domain;

namespace Lms.CourseManagement.Domain.Entities
{
    public class LessonTag : AggregateRoot <Guid>
    {

        public string? TagName { get; private set; }

        public LessonTag() { } // EF core

        private LessonTag(string name)
        {
            TagName = name.Trim();
        }

        public static LessonTag Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tag name is required. ", nameof(name));


            return new LessonTag (name);
        }
    }
}
