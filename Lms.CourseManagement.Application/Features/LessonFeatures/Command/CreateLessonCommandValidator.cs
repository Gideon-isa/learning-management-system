using FluentValidation;
using Lms.CourseManagement.Application.Features.Lesson.Command;

namespace Lms.CourseManagement.Application.Features.LessonFeatures.Command
{
    public class CreateLessonCommandValidator : AbstractValidator<CreateLessonCommand>
    {
        public CreateLessonCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Lesson title is required.")
                .MaximumLength(200).WithMessage("Lesson title must not exceed 200 characters.");
        }
    }
}
