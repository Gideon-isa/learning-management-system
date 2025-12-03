using FluentValidation;
using Lms.Shared.Abstractions.Interfaces.Request;

namespace Lms.CourseManagement.Application.Features.Module.Commands
{
    public class CreateCourseModuleCommandValidator : AbstractValidator<CreateCourseModuleCommand>
    {
        public CreateCourseModuleCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.");
        }
    }
}
