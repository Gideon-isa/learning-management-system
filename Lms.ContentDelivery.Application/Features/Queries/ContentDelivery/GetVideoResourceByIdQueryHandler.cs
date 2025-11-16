using Lms.ContentDelivery.Application.Abstractions;
using Lms.ContentDelivery.Application.Features.Queries.Dto;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.ContentDelivery.Application.Features.Queries.ContentDelivery
{
    public class GetVideoResourceByIdQueryHandler : ICustomRequestHandler<GetVideoResourceByIdQuery, IResponseWrapper<StreamLessonVideoResponse>>
    {
        private readonly ICourseVideoRespository _courseVideoRespository;

        public GetVideoResourceByIdQueryHandler(ICourseVideoRespository courseVideoRespository)
        {
            _courseVideoRespository = courseVideoRespository;
        }

        public async Task<IResponseWrapper<StreamLessonVideoResponse>> Handle(GetVideoResourceByIdQuery request, CancellationToken cancellationToken)
        {
            var video = await _courseVideoRespository.GetVideoMetadataByIdAsync(request.VideoId, cancellationToken) 
                ?? throw new Exception($"No video with id {request.VideoId}");

            if (!File.Exists(video.FilePath))
                throw new Exception("No file path");

            var response = new StreamLessonVideoResponse
            {
                Path = video.FilePath,
                ContentType = video.ContentType,
                FileName = Path.GetFileName(video.FilePath),
            };
            return await ResponseWrapper<StreamLessonVideoResponse>.SuccessAsync(response);
        }
    }
}