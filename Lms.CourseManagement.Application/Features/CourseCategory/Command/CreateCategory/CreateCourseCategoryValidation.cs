using FluentValidation;

namespace Lms.CourseManagement.Application.Features.CourseCategory.Command.CreateCategory
{
    public class CreateCourseCategoryValidation : AbstractValidator<CreateCourseCategoryCommand>
    {
        public CreateCourseCategoryValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Course category name is required.")
                .MaximumLength(100)
                .WithMessage("Course category name must not exceed 100 characters.");
        }
    }
}
