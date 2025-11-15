using Lms.ContentDelivery.Application.Features.Queries.StudentModule;
using Lms.ContentDelivery.Domain.Repositories;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.ContentDelivery.Application.Features.Queries.ContentDelivery
{
    public class GetModuleByIdQueryHandler : ICustomRequestHandler<GetMouduleByIdQuery, IResponseWrapper<List<StudentModuleResponse>>>
    {
        private readonly ICourseContentRepository _courseContentRepository;

        public GetModuleByIdQueryHandler(ICourseContentRepository courseContentRepository)
        {
            _courseContentRepository = courseContentRepository;
        }

        public async Task<IResponseWrapper<List<StudentModuleResponse>>> Handle(GetMouduleByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _courseContentRepository.GetCourseContentByCourseId(request.CourseId, cancellationToken);
            throw new NotImplementedException();
            
        }
    }
}
