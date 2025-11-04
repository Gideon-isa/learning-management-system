using Lms.CourseManagement.Application.Features.Course.DTO;
using Lms.CourseManagement.Domain.Repositories;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using Mapster;

namespace Lms.CourseManagement.Application.Features.Course.Queries.GetCourseById
{
    public class GetCourseByIdQueryHandler : ICustomRequestHandler<GetCourseByIdQuery, IResponseWrapper>
    {
        private ICourseRepository _courseRepository;

        public GetCourseByIdQueryHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IResponseWrapper> Handle(GetCourseByIdQuery query, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(query.CourseId, cancellationToken);
            if (course == null)
            {
                //TODO: use custom exception
                throw new Exception();
            }
            var courseDto = course.Adapt<CourseResponse>();
            return await ResponseWrapper<CourseResponse>.SuccessAsync(courseDto);
        }
    }
}
