using Lms.CourseManagement.Application.Features.LessonFeatures.Dto;
using Lms.CourseManagement.Application.Features.LessonFeatures.Quries;
using Lms.CourseManagement.Domain.Repositories;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using Mapster;

namespace Lms.CourseManagement.Application.Features.LessonFeatures.Query
{
    public class GetLessonByIdQueryHandler : ICustomRequestHandler<GetLessonByIdQuery, IResponseWrapper<LessonResponse>>
    {
        private readonly ILessonRespositoy _lessonRepository;

        public GetLessonByIdQueryHandler(ILessonRespositoy lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }
        public async Task<IResponseWrapper<LessonResponse>> Handle(GetLessonByIdQuery request, CancellationToken cancellationToken)
        {
            // TODO: to ue custom exception
            var lesson = await _lessonRepository.GetByIdAsync(request.Id, cancellationToken) ?? throw new Exception();
            var lessonResponse = lesson.Adapt<LessonResponse>();
            return await ResponseWrapper<LessonResponse>.SuccessAsync(data: lessonResponse);
        }
    }
}
