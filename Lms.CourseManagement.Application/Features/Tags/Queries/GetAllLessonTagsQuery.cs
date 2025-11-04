using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.CourseManagement.Application.Features.Tags.Queries
{
    public class GetAllLessonTagsQuery : ICustomRequest<IResponseWrapper<LessonTagResponses>>
    {
    }
}
