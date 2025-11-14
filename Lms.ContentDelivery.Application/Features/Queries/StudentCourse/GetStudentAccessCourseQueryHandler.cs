using Lms.ContentDelivery.Domain.Entities;
using Lms.ContentDelivery.Domain.Repositories;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using Mapster;

namespace Lms.ContentDelivery.Application.Features.Queries.StudentCourse
{
    public class GetStudentAccessCourseQueryHandler : ICustomRequestHandler<GetStudentAccessCourseQuery, IResponseWrapper<StudentAccessCourseResponse>>
    {
        private readonly ICourseContentRepository _courseContentRepository;
        private readonly IStudentAccessRepository _studentAccessRepository;

        public GetStudentAccessCourseQueryHandler(ICourseContentRepository courseContent, IStudentAccessRepository studentAccessRepository)
        {
            _courseContentRepository = courseContent;
            _studentAccessRepository = studentAccessRepository;
        }

        public async Task<IResponseWrapper<StudentAccessCourseResponse>> Handle(GetStudentAccessCourseQuery request, CancellationToken cancellationToken)
        {
            // Get student access by studentCode

            var studentAccess = await _studentAccessRepository.GetStudentAllAccessAsync(request.StudentCode, cancellationToken);

            var courseIds = studentAccess.Select(st => st.CourseId).ToList();

            var courses = await _courseContentRepository.GetCourseContentsByCourseIdsAsync(courseIds, cancellationToken);

            var coursesDtos = courses.Select(
                course => new CourseContentDto {
                    CourseId = course.CourseId,
                    CourseTitle = course.CourseTitle,
                    CourseCode = course.CourseCode,
                    Modules =
                    [..course.Modules.Select(
                        module => new CourseModuleContentDto
                        {
                            CourseModuleId = module.Id,
                            ModuleTitle = module.Title,
                            Lessons = [..module.Lessons.Select(
                                lesson => new LessonDto
                                {
                                    Title = lesson.Title,
                                    Duration = lesson.Duration,
                                    Videos = [..lesson.Videos.Select(
                                        video => new VideoDto 
                                        { 
                                            Path = video.Path, 
                                            Title = video.Title, 
                                            Thumbnail = video.Thumbnail
                                        })]
                                })]
                        })
                    ]
                });

            var courseDtos = new StudentAccessCourseResponse { StudentCode = request.StudentCode, Courses = [..coursesDtos] };
            //var courseDtos = courses.Adapt<List<CourseContentDto>>();
            var studentAccessDto = new StudentAccessCourseResponse { StudentCode = request.StudentCode, Courses = [..coursesDtos]};
            return await ResponseWrapper<StudentAccessCourseResponse>.SuccessAsync(data: studentAccessDto, [$"{coursesDtos.ToList().Count}"]);
        }
    }
}
