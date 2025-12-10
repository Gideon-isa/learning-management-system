using Lms.SharedKernel.Domain;

namespace Lms.CourseManagement.Domain.Entities
{
    public class CourseCategory : AggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Code { get; set; }

        private CourseCategory() { } // EF Core requirement

        private CourseCategory(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public static CourseCategory Create(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Category name cannot be null or empty.", nameof(name));
            var courseCategory = new CourseCategory(name);
            courseCategory.GenerateCode();
            return courseCategory;
        }

        private void GenerateCode()
        {
            var formattedCode  = Name.Trim().Split(" ")[0][..3];
            Code = $"{formattedCode.ToUpperInvariant()}-{Guid.NewGuid().ToString().Split('-')[0].ToUpperInvariant()}";
        }
    }


}
