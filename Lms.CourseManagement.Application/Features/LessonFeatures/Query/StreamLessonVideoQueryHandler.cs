using Lms.CourseManagement.Application.Abstractions;
using Lms.CourseManagement.Application.Features.LessonFeatures.Dto;
using Lms.CourseManagement.Domain.Repositories;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.CourseManagement.Application.Features.LessonFeatures.Query
{
    public class StreamLessonVideoQueryHandler : ICustomRequestHandler<StreamLessonVideoQuery, IResponseWrapper<StreamLessonVideoResponse>>
    {
        private readonly ILessonRespositoy _lessonRepository;
        private readonly IMimeTypeService _mimeTypeService;
        //private readonly IVideoStroageService videoStroageService;
        public StreamLessonVideoQueryHandler(ILessonRespositoy lessonRepository, IMimeTypeService mimeTypeService)
        {
            _lessonRepository = lessonRepository;
            _mimeTypeService = mimeTypeService;
        }

        public async Task<IResponseWrapper<StreamLessonVideoResponse>> Handle(StreamLessonVideoQuery request, CancellationToken cancellationToken)
        {
            var lesson = await _lessonRepository.GetByIdAsync(request.LessonId, cancellationToken) ?? throw new Exception();

            var video = lesson.Videos.FirstOrDefault(v => v.Title == request.VideoTitle) ?? throw new Exception();
            if (!File.Exists(video.Path)) throw new Exception("");

            var response = new StreamLessonVideoResponse
            {
                Path = video.Path,
                ContentType = _mimeTypeService.GetMimeType(video.Path),
                FileName = Path.GetFileName(video.Path)
            };
            return await ResponseWrapper<StreamLessonVideoResponse>.SuccessAsync(data: response);
        }
    }


}
