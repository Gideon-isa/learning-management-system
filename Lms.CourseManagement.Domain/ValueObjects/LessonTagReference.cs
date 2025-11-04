namespace Lms.CourseManagement.Domain.ValueObjects
{
    public record class LessonTagReference
    {
        public Guid TagId { get; private set; }
        public string TagName { get; private set; }
        private LessonTagReference() { } // EF core
        public LessonTagReference(Guid tagId, string tagName)
        {
            TagId = tagId;
            TagName = tagName;
        }
    }
}
