using FluentValidation;
using Lms.CourseManagement.Application.Features.Tags.Command;

namespace Lms.CourseManagement.Application.Features.Tags.Commands
{
    public class CreateLessonTagCommandValidator : AbstractValidator<CreateLessonTagCommand>
    {
        public CreateLessonTagCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tag name is required.")
                .MaximumLength(100).WithMessage("Tag name must not exceed 100 characters.");
        }
    }
}
