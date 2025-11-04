using Lms.CourseManagement.Application.Features.LessonFeatures.Dto;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.CourseManagement.Application.Features.LessonFeatures.Quries
{
    public class GetLessonByIdQuery : ICustomRequest<IResponseWrapper<LessonResponse>>
    {
        public Guid Id { get; set; }
    }
}
