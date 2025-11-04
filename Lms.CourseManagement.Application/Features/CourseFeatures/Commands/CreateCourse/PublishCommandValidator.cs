using FluentValidation;
using Lms.CourseManagement.Application.Features.Course.Commands.PublishCourse;

namespace Lms.CourseManagement.Application.Features.CourseFeatures.Commands.CreateCourse
{
    public class PublishCommandValidator : AbstractValidator<PublishCourseCommand>
    {
        public PublishCommandValidator()
        {
            RuleFor(c => c.CourseId)
                .NotEmpty();
            
        }
    }
}
