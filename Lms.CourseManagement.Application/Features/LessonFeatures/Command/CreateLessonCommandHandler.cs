using Lms.CourseManagement.Application.Abstractions;
using Lms.CourseManagement.Application.Features.Lesson.Command;
using Lms.CourseManagement.Domain.Entities;
using Lms.CourseManagement.Domain.Repositories;
using Lms.CourseManagement.Domain.ValueObjects;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.CourseManagement.Application.Features.LessonFeatures.Command
{
    public class CreateLessonCommandHandler : ICustomRequestHandler<CreateLessonCommand, IResponseWrapper>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseManagementUnitOfWork _unitOfWork;
        private readonly ILessonTagRespository _tagRespository;
        private readonly IFileStorageService _fileStorageService;

        public CreateLessonCommandHandler(ICourseRepository courseRepository, ICourseManagementUnitOfWork unitOfWork, 
            IFileStorageService fileStorageService, ILessonTagRespository tagRespository)
        {
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
            _tagRespository = tagRespository;
        }

        public async Task<IResponseWrapper> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
        {
            var course = await  _courseRepository.GetByIdAsync(request.CourseId, cancellationToken);
            
            if (course == null)
            {
                // TODO: To use custom exception
                throw new Exception("Course not found");
            }
            var module = course.GetModuleById(request.ModuleId) ?? throw new Exception("Module not found");

            IEnumerable<LessonTag> lessonTags = [];

            if (request?.TagIds?.Any() == true)
            {
                lessonTags = await _tagRespository.GetByIdsAsync(request.TagIds, cancellationToken);
            }

            string? videoFilePath = null;
            IEnumerable<LessonImage> lessonImages = [];
            if (request.Video?.Content != null)
            {
                videoFilePath = await _fileStorageService.UploadVideoFileAsync(request, cancellationToken);
                lessonImages = await _fileStorageService.UploadImageFileAsync(request, cancellationToken);
            }

            var newLesson = Domain.Entities.Lesson.Create(
                    lessonTitle: request.Title,
                    lessonDescription: request.Description,
                    lessonCourseDuration: request.Duration,
                    videoPath: videoFilePath,
                    videoTitle: request.Video.Title,
                    videoThumbNail: request.Video.ThumbNail,
                    videoDescription: request.Video.Description,
                    lessonImages: lessonImages, 
                    lessonTags: lessonTags);

            course.AddLessonToModule(module.Id, newLesson);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            //module.Lessons.Adapt<LessonResponse>();
            return await ResponseWrapper.SuccessAsync("Lesson created successfully");
        }
    }
}
