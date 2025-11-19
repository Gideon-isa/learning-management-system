using Lms.CourseManagement.Application.Abstractions;
using Lms.CourseManagement.Domain.Repositories;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using Lms.SharedKernel.Interfaces;
using Mapster;

namespace Lms.CourseManagement.Application.Features.Tags.Queries
{
    public class GetLessonTagByIdQueryCommandHandler : ICustomRequestHandler<GetLessonTagByIdQuery, IResponseWrapper<LessonTagResponse>>
    {
        private readonly ILessonTagRespository _lessonTagRespository;
        
        
        public GetLessonTagByIdQueryCommandHandler(ILessonTagRespository lessonTagRespository)
        {
            _lessonTagRespository = lessonTagRespository;
        }

        public async Task<IResponseWrapper<LessonTagResponse>> Handle(GetLessonTagByIdQuery request, CancellationToken cancellationToken)
        {
            var tag = await _lessonTagRespository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new Exception();

            var tagDto = tag.Adapt<LessonTagResponse>();
            return await ResponseWrapper<LessonTagResponse>.SuccessAsync(data: tagDto, messages: ["Content tag retrieved successfully"]);
        }
    }
}
