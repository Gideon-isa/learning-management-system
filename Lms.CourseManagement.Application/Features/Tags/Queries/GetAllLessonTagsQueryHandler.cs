using Lms.CourseManagement.Domain.Repositories;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using Mapster;

namespace Lms.CourseManagement.Application.Features.Tags.Queries
{
    public class GetAllLessonTagsQueryHandler : ICustomRequestHandler<GetAllLessonTagsQuery, IResponseWrapper<LessonTagResponses>>
    {
        private readonly ILessonTagRespository _lessonTagRespository;

        public GetAllLessonTagsQueryHandler(ILessonTagRespository lessonTagRespository)
        {
            _lessonTagRespository = lessonTagRespository;
        }

        public async Task<IResponseWrapper<LessonTagResponses>> Handle(GetAllLessonTagsQuery request, CancellationToken cancellationToken)
        {
            var lessonTagsinDb = await _lessonTagRespository.GetAllAsync(cancellationToken);
            var lessonTags = lessonTagsinDb.Adapt<List<LessonTagResponse>>();
            var lessonTagDto = new LessonTagResponses { LessonTags =  lessonTags };
            return await ResponseWrapper<LessonTagResponses>.SuccessAsync(data: lessonTagDto, [$"{lessonTags.Count}"]);
        }
    }
}
