using Lms.CourseManagement.Application.Features.Course.DTO;
using Lms.CourseManagement.Domain.Entities;
using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.SharedKernel.Common.Wrappers;

namespace Lms.CourseManagement.Application.Features.CourseFeatures.Commands.CreateCourse
{
    public class CreateCourseCommand : ICustomRequest<IResponseWrapper<CourseResponse>>
    {
        public required string CourseTitle { get; set; }
        public required string CourseCode { get; set; }
        public required string Description { get; set; }
        public required Guid CategoryId { get; set; }
        public required Guid InstructorId { get; set; }
        public DateTime? PublishedOn { get; set; }
        
    }
}
