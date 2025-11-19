using Lms.CourseManagement.Application.Abstractions;
using Lms.CourseManagement.Application.Features.Lesson.Command;
using Lms.CourseManagement.Application.Models;
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
        private readonly IVideoMetadataRepository _videoMetadataRepository;

        public CreateLessonCommandHandler(ICourseRepository courseRepository, ICourseManagementUnitOfWork unitOfWork,
            IFileStorageService fileStorageService, ILessonTagRespository tagRespository, IVideoMetadataRepository videoMetadataRepository)
        {
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
            _tagRespository = tagRespository;
            _videoMetadataRepository = videoMetadataRepository;
        }

        public async Task<IResponseWrapper> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
        {
            string? videoFilePath = null;
            var videoId = Guid.NewGuid();
            IEnumerable<LessonImage> lessonImages = [];
            IEnumerable<LessonTag> lessonTags = [];

            var course = await  _courseRepository.GetByIdAsync(request.CourseId, cancellationToken);
            
            if (course == null)
                // TODO: To use custom exception
                throw new Exception("Course not found");
            
            
            var module = course.GetModuleById(request.ModuleId) ?? throw new Exception("Module not found");

            if (request?.TagIds?.Any() == true)
            {
                lessonTags = await _tagRespository.GetByIdsAsync(request.TagIds, cancellationToken);
            }

            if (request.Video?.Content != null)
            {
                videoFilePath = await _fileStorageService.UploadVideoFileAsync(request, cancellationToken);
                lessonImages = await _fileStorageService.UploadImageFileAsync(request, cancellationToken);
            }

            var videoMetadata = new VideoMetadata
            {
                Id = videoId,
                FilePath = videoFilePath,
                ContentType = request.Video.ContentType,
                FileSize = request.Video.FileSize,
            };

            var newLesson = Domain.Entities.Content.Create(
                    lessonTitle: request.Title,
                    lessonDescription: request.Description,
                    lessonCourseDuration: request.Duration,
                    videoId: videoId, // videoId
                    videoPath: videoFilePath,
                    videoTitle: request.Video.Title,
                    videoThumbNail: request.Video.ThumbNail,
                    videoDescription: request.Video.Description,
                    lessonImages: lessonImages, 
                    lessonTags: lessonTags);

            course.AddLessonToModule(module.Id, newLesson);
            await _videoMetadataRepository.AddAsync(videoMetadata, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            //module.Lessons.Adapt<LessonResponse>();
            return await ResponseWrapper.SuccessAsync("Content created successfully");
        }
    }
}
