using FluentValidation;

namespace Lms.CourseManagement.Application.Features.CourseFeatures.Commands.CreateCourse
{
    public class CreateCourseValidator : AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseValidator()
        {
            RuleFor(u => u.CourseTitle)
                .NotEmpty();
        }
    }
}
