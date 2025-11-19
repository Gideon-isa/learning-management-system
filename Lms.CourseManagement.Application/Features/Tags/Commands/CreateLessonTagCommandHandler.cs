using Lms.CourseManagement.Application.Abstractions;
using Lms.CourseManagement.Application.Features.Tags.Command;
using Lms.CourseManagement.Domain.Entities;
using Lms.CourseManagement.Domain.Repositories;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;
using Mapster;

namespace Lms.CourseManagement.Application.Features.Tags.Commands
{
    public class CreateLessonTagCommandHandler : ICustomRequestHandler<CreateLessonTagCommand, IResponseWrapper<LessonTagResponse>>
    {
        private readonly ILessonTagRespository _lessonTagRespository;
        private readonly ICourseManagementUnitOfWork _unitOfWork;
        public CreateLessonTagCommandHandler(ILessonTagRespository lessonTagRespository, ICourseManagementUnitOfWork unitOfWork)
        {
            _lessonTagRespository = lessonTagRespository;
            _unitOfWork = unitOfWork;
        }
        public async Task<IResponseWrapper<LessonTagResponse>> Handle(CreateLessonTagCommand request, CancellationToken cancellationToken)
        {
            var newTag = LessonTag.Create(request.Name);
            await _lessonTagRespository.AddAsync(newTag, cancellationToken);
            await _unitOfWork.SaveChangesAsync();
            var tagDto = newTag.Adapt<LessonTagResponse>();
            return await ResponseWrapper<LessonTagResponse>.SuccessAsync(data: tagDto, messages: ["Content tag created successfully"]);
        }
    }
}
