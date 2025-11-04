using Lms.Identity.Application.Events;
using Lms.Identity.Application.Features.Identity.Users;
using Lms.Identity.Infrastructure.Identity.Models;
using Mapster;

namespace Lms.Identity.Infrastructure.Mapping
{
    public static class MappingConfig
    {
        public static void RegisterConfig()
        {
            TypeAdapterConfig<ApplicationUser, UserResponse>
                .NewConfig()
                .Map(dest => dest.InstructorStatus, src => src.InstructorStatus.ToString());


            // Configuring the UserResponse to ApprovedInstructorEvent mapping as ApprovedInstructorEvent is a record
            //TypeAdapterConfig<UserResponse, ApprovedInstructorEvent>.NewConfig()
            //    .MapWith(src => new ApprovedInstructorEvent(
                    
            //        src.FirstName ?? string.Empty,
            //        src.LastName ?? string.Empty,
            //        src.DisplayName ?? string.Empty,
            //        src.ProfileImageUrl ?? string.Empty,
            //        src.Bio ?? string.Empty,
            //        src.Department ?? string.Empty,
            //        src.Institution ?? string.Empty,
            //        src.StaffId ?? string.Empty));
        }
    }
}
