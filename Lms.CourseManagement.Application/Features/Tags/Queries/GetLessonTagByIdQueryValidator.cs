using FluentValidation;

namespace Lms.CourseManagement.Application.Features.Tags.Queries
{
    public class GetLessonTagByIdQueryValidator : AbstractValidator<GetLessonTagByIdQuery>
    {
        public GetLessonTagByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Tag ID is required.");
        }
    }
}
