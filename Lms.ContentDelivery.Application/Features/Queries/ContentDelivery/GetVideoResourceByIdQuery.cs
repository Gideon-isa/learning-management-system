using Lms.ContentDelivery.Application.Features.Queries.Dto;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.ContentDelivery.Application.Features.Queries.ContentDelivery
{
    public class GetVideoResourceByIdQuery : ICustomRequest<IResponseWrapper<StreamLessonVideoResponse>>
    {
        public Guid VideoId { get; set; }

    }
}
